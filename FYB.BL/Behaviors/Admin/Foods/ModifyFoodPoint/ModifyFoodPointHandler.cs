using FYB.BL.Exceptions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.ModifyFoodPoint;

public class ModifyFoodPointHandler : IRequestHandler<ModifyFoodPointCommand>
{
    private readonly DataContext _context;

    public ModifyFoodPointHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ModifyFoodPointCommand request, CancellationToken cancellationToken)
    {
        var foodPoint = await _context.FoodPoints.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (foodPoint is null) throw new NotFoundException(ErrorMessages.FoodPointNotFound);

        foodPoint.Title = request.Title;
        foodPoint.Description = request.Description;
        foodPoint.PortionMass = request.PortionMass;
        foodPoint.CoockingMethod = request.CoockingMethod;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
