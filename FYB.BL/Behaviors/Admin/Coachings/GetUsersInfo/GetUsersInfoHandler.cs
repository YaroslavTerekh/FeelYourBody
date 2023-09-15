using AutoMapper;
using FYB.Data.Common.DataTransferObjects;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.GetUsersInfo;

public class GetUsersInfoHandler : IRequestHandler<GetUsersInfoQuery, List<UserDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetUsersInfoHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserDTO>> Handle(GetUsersInfoQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
                .AsNoTracking()
                .Include(t => t.CoachingPurchases)
                    .ThenInclude(t => t.Product)
                .Include(t => t.Coachings)
                    .ThenInclude(t => t.Coach)
                .Include(t => t.Coachings)
                    .ThenInclude(t => t.Food)
                .Include(t => t.Coachings)
                    .ThenInclude(t => t.CoachingPhoto)
                .Include(t => t.Coachings)
                    .ThenInclude(t => t.CoachingDetailParents)
                    .ThenInclude(t => t.Details)
                .Include(t => t.Coachings)
                    .ThenInclude(t => t.Feedbacks)
                .Include(t => t.Coachings)
                    .ThenInclude(t => t.ExamplePhotos)
                .Include(t => t.Coachings)
                    .ThenInclude(t => t.Videos)
                .Include(t => t.Foods)
                    .ThenInclude(t => t.FoodPointParents)
                    .ThenInclude(t => t.FoodPoints)
                .Include(t => t.FoodPurchases)
                .Select(t => new UserDTO
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Role = t.Role.ToString(),
                    RegisterDate = t.RegisterDate,
                    FoodPurchases = t.FoodPurchases.Select(fp => new Purchase<FoodDTO>
                    {
                        CreatedDate = fp.CreatedDate,
                        ExpireDate = fp.ExpireDate,
                        IsExpired = fp.IsExpired,
                        Id = fp.Id,
                        OrderId = fp.OrderId,
                        ProductId = fp.ProductId,
                        Product = _mapper.Map<FoodDTO>(fp.Product),
                        ProductType = fp.ProductType,
                        User = fp.User,
                        UserId = fp.UserId,
                    }).ToList(),
                    CoachingPurchases = t.CoachingPurchases.Select(fp => new Purchase<CoachingDTO>
                    {
                        CreatedDate = fp.CreatedDate,
                        ExpireDate = fp.ExpireDate,
                        IsExpired = fp.IsExpired,
                        Id = fp.Id,
                        OrderId = fp.OrderId,
                        ProductId = fp.ProductId,
                        Product = _mapper.Map<CoachingDTO>(fp.Product),
                        ProductType = fp.ProductType,
                        User = fp.User,
                        UserId = fp.UserId,
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

        return users;
    }
}
