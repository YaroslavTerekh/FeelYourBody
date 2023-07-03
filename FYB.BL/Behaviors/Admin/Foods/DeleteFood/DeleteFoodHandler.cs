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

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFood;

public class DeleteFoodHandler : IRequestHandler<DeleteFoodCommand>
{
    private readonly DataContext _context;

    public DeleteFoodHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _context.Food.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (food is null) throw new NotFoundException(ErrorMessages.FoodNotFound);

        _context.Food.Remove(food);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
