using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.BL.Settings.Abstractions;
using FYB.Data.Common;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Realizations;

public class LiqPayService : ILiqPayService
{
    private readonly DataContext _context;
    private readonly ILiqPaySettings _liqPaySettings;
    private readonly IUnixService _unixService;
    private readonly IHangfireJobsService _hangfireJobsService;

    public LiqPayService(
        DataContext context, 
        ILiqPaySettings liqPaySettings, 
        IUnixService unixService, 
        IHangfireJobsService hangfireJobsService
    )
    {
        _context = context;
        _unixService = unixService;
        _liqPaySettings = liqPaySettings;
        _hangfireJobsService = hangfireJobsService;
    }

    public LiqPayResponse DecodeResponse(Dictionary<string, string> data)
    {
        byte[] bytes = Convert.FromBase64String(data["data"]);

        string jsonString = Encoding.UTF8.GetString(bytes);

        LiqPayResponse response = JsonConvert.DeserializeObject<LiqPayResponse>(jsonString);

        return response;
    }

    public async Task<string> GenerateForm(Guid goodId, Guid currentUserId, PurchaseProductType productType, CancellationToken cancellationToken = default)
    {
        BaseProduct? good = productType switch 
        { 
            PurchaseProductType.Coaching => await _context.Coachings.FirstOrDefaultAsync(t => t.Id == goodId, cancellationToken),
            PurchaseProductType.Food => await _context.Food.FirstOrDefaultAsync(t => t.Id == goodId, cancellationToken),
            _ => null,
        };

        if (good is null) throw new NotFoundException(ErrorMessages.ProductNotFound(productType.ToString()));

        string orderId = null;

        if (productType == PurchaseProductType.Coaching)
        {
            var purchase = new Purchase<Coaching>
            {
                OrderId = Guid.NewGuid().ToString(),
                ExpireDate = _unixService.GenerateExpireDate(_unixService.GenerateUnix(good.UnixExpireTime)),
                ProductId = good.Id,
                ProductType = productType,
                UserId = currentUserId
            };

            orderId = purchase.OrderId;
            await _context.CoachingPurchases.AddAsync(purchase, cancellationToken);
        }

        if (productType == PurchaseProductType.Food)
        {
            var purchase = new Purchase<Food>
            {
                OrderId = Guid.NewGuid().ToString(),
                ExpireDate = _unixService.GenerateExpireDate(_unixService.GenerateUnix(good.UnixExpireTime)),
                ProductId = good.Id,
                ProductType = productType,
                UserId = currentUserId
            };

            orderId = purchase.OrderId;
            await _context.FoodPurchases.AddAsync(purchase, cancellationToken);
        }

        if (orderId is null) throw new Exception(ErrorMessages.UnknownError);

        await _context.SaveChangesAsync(cancellationToken);

        var invoiceRequest = new LiqPayRequest
        {
            PublicKey = _liqPaySettings.PublicKey,
            Version = 3,
            Amount = good.Price,
            Currency = "UAH",
            OrderId = orderId,
            Action = LiqPayRequestAction.Pay,
            Language = LiqPayRequestLanguage.UK,
            Description = $"Покупка спортивного курсу: {good.Title}",
            ServerUrl = _liqPaySettings.ServerUrl,
        };

        var liqPayClient = new LiqPayClient(_liqPaySettings.PublicKey, _liqPaySettings.PrivateKey);
        return liqPayClient.CNBForm(invoiceRequest);
    }

    public async Task ProcessCallbackAsync(Dictionary<string, string> data, CancellationToken cancellationToken)
    {
        var responseData = DecodeResponse(data);

        if (responseData.Status == LiqPayResponseStatus.Success)
        {
            List<BasePurchase> purchases = new List<BasePurchase>();

            var coachingPurchases = await _context.CoachingPurchases.ToListAsync(cancellationToken);
            var foodPurchases = await _context.FoodPurchases.ToListAsync(cancellationToken);

            purchases.AddRange(coachingPurchases);
            purchases.AddRange(foodPurchases);

            var purchase = purchases.FirstOrDefault(t => t.OrderId == responseData.OrderId);

            if (purchase is null) throw new NotFoundException(ErrorMessages.PurchaseNotFound);

            BaseProduct? product = purchase.GetType() == typeof(Purchase<Food>) ?
                await _context.Food.FirstOrDefaultAsync(t => t.Id == purchase.ProductId) :
                await _context.Coachings.FirstOrDefaultAsync(t => t.Id == purchase.ProductId);

            if (product is null) throw new NotFoundException(ErrorMessages.ProductNotFound("Продукту з замовлення"));

            if(purchase.GetType() == typeof(Purchase<Coaching>))
            {
                var coaching = await _context.Coachings
                    .FirstOrDefaultAsync(t => t.Id == purchase.ProductId);

                if (coaching is null) throw new NotFoundException(ErrorMessages.ProductNotFound("Продукту з замовлення"));

                if (coaching.FoodId != null)
                {
                    var user = await _context.Users
                                .Include(t => t.CoachingPurchases)
                                .Include(t => t.FoodPurchases)
                                .FirstOrDefaultAsync(t => t.Id == purchase.UserId, cancellationToken);

                    if (user is null) throw new NotFoundException(ErrorMessages.UserNotFound);

                    var food = await _context.Food
                        .Include(t => t.Users)
                        .FirstOrDefaultAsync(t => t.Id == coaching.FoodId);

                    food.Users.Add(user);
                }
            }

            await AssignProductToUserAsync(product, purchase.UserId, cancellationToken);
            _hangfireJobsService.CreateJobForExpiringProduct(product, purchase.UserId, purchase.OrderId);
        }
    }

    private async Task AssignProductToUserAsync(BaseProduct product, Guid currentUserId, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .Include(t => t.CoachingPurchases)
            .Include(t => t.FoodPurchases)
            .FirstOrDefaultAsync(t => t.Id == currentUserId, cancellationToken);

        if (user is null) throw new NotFoundException(ErrorMessages.UserNotFound);

        product.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
