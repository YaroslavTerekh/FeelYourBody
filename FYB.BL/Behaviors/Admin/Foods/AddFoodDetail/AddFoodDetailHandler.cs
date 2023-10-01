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

namespace FYB.BL.Behaviors.Admin.Foods.AddFoodDetail;

public class AddFoodDetailHandler : IRequestHandler<AddFoodDetailCommand>
{
    private readonly DataContext _context;

    public AddFoodDetailHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(AddFoodDetailCommand request, CancellationToken cancellationToken)
    {
        var food = await _context.Food
            .Include(t => t.FoodDetails)
            .FirstOrDefaultAsync(t => t.Id == request.FoodId, cancellationToken);

        if (food is null)
            throw new NotFoundException(ErrorMessages.FoodNotFound);

        var detail = new FoodDetail 
        {
            Title = request.Title,
            Detail = request.Detail,
            FoodId = food.Id
        };

        await _context.FoodDetail.AddAsync(detail, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
