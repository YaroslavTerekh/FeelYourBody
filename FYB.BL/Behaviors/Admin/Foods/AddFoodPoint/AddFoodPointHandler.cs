using FYB.BL.Exceptions;
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

namespace FYB.BL.Behaviors.Admin.Foods.AddFoodPoint;

public class AddFoodPointHandler : IRequestHandler<AddFoodPointCommand>
{
    private readonly DataContext _context;

    public AddFoodPointHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddFoodPointCommand request, CancellationToken cancellationToken)
    {
        var food = await _context.Food
            .FirstOrDefaultAsync(t => t.Id == request.FoodId, cancellationToken);

        if (food is null)
            throw new NotFoundException(ErrorMessages.FoodNotFound);
        
        var foodPoint = new FoodPoint
        {
            Title = request.Title,
            Description = request.Description,
            PortionMass = request.PortionMass,
            CoockingMethod = request.CoockingMethod,
            FoodId = request.FoodId,
        };

        await _context.FoodPoints.AddAsync(foodPoint, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
