using AutoMapper;
using FYB.BL.Exceptions;
using FYB.Data.Common;
using FYB.Data.Common.DataTransferObjects;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Foods.GetFood;

public class GetFoodHandler : IRequestHandler<GetFoodQuery, FoodDTO>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetFoodHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FoodDTO> Handle(GetFoodQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == request.CurrentUserId, cancellationToken);

        if (user is null) throw new NotFoundException(ErrorMessages.UserNotFound);

        var food = await _context.Food
            .Include(t => t.Users)
            .Include(t => t.FoodPointParents.OrderBy(t => t.DayNumber))
                .ThenInclude(t => t.FoodPoints)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (food is null) throw new NotFoundException(ErrorMessages.FoodNotFound);
        if (!food.Users.Contains(user) && user.Role != Role.Admin) throw new Exception(ErrorMessages.ContentAccessForbidden);

        return _mapper.Map<FoodDTO>(food);
    }
}
