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

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFoodPoint;

public class DeleteFoodPointHandler : IRequestHandler<DeleteFoodPointCommand>
{
    private readonly DataContext _context;

    public DeleteFoodPointHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFoodPointCommand request, CancellationToken cancellationToken)
    {
        var foodPoint = await _context.FoodPoints.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (foodPoint is null) throw new NotFoundException(ErrorMessages.FoodPointNotFound);

        _context.FoodPoints.Remove(foodPoint);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
