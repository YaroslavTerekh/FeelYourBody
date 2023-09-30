using AutoMapper;
using FYB.BL.Exceptions;
using FYB.Data.Common.DataTransferObjects;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.GetUser;

public class GetUserHandler : IRequestHandler<GetUserQuery, UserDTO>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetUserHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
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
                .ThenInclude(t => t.FoodPoints)
                .Include(t => t.FoodPurchases)
                .Select(t => new UserDTO
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Role = t.Role.ToString(),
                    RegisterDate = t.RegisterDate,
                    Foods = t.Foods.Select(f => _mapper.Map<FoodDTO>(f)).ToList(),
                    Coachings = t.Coachings.Select(c => _mapper.Map<CoachingDTO>(c)).ToList(),
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
                .FirstOrDefaultAsync(t => t.Id == request.UserId, cancellationToken);

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        return user;
    }
}
