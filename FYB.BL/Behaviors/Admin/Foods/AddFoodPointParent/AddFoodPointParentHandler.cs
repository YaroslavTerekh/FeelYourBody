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

namespace FYB.BL.Behaviors.Admin.Foods.AddFoodPointParent;

public class AddFoodPointParentHandler : IRequestHandler<AddFoodPointParentCommand, Guid>
{
    private readonly DataContext _context;

    public AddFoodPointParentHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(AddFoodPointParentCommand request, CancellationToken cancellationToken)
    {
        var food = await _context.Food
            .FirstOrDefaultAsync(t => t.Id == request.FoodId, cancellationToken);

        if (food is null)
            throw new NotFoundException(ErrorMessages.FoodNotFound);

        var foodPointParent = new FoodPointParent
        {
            DayNumber = request.DayNumber,
            FoodId = request.FoodId,
        };

        await _context.FoodPointParents.AddAsync(foodPointParent, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return foodPointParent.Id;
    }
}
