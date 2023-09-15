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

namespace FYB.BL.Behaviors.Admin.Foods.ModifyFoodPointParent;

public class ModifyFoodPointParentHandler : IRequestHandler<ModifyFoodPointParentCommand>
{
    private readonly DataContext _context;

    public ModifyFoodPointParentHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ModifyFoodPointParentCommand request, CancellationToken cancellationToken)
    {
        var parent = await _context.FoodPointParents
            .FirstOrDefaultAsync(t => t.Id == request.FoodPointParentId, cancellationToken);

        if (parent is null)
            throw new NotFoundException(ErrorMessages.FoodPointNotFound);

        parent.DayNumber = request.DayNumber;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
