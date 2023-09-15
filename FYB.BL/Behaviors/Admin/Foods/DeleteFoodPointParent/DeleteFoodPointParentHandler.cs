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

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFoodPointParent;

public class DeleteFoodPointParentHandler : IRequestHandler<DeleteFoodPointParentCommand>
{
    private readonly DataContext _context;

    public DeleteFoodPointParentHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFoodPointParentCommand request, CancellationToken cancellationToken)
    {
        var parent = await _context.FoodPointParents
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (parent is null)
            throw new NotFoundException(ErrorMessages.FoodPointNotFound);

        _context.FoodPointParents.Remove(parent);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
