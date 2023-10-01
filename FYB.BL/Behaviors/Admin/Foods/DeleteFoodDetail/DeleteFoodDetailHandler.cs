using FYB.BL.Exceptions;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFoodDetail;

public class DeleteFoodDetailHandler : IRequestHandler<DeleteFoodDetailCommand>
{
    private readonly DataContext _context;

    public DeleteFoodDetailHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFoodDetailCommand request, CancellationToken cancellationToken)
    {
        var foodDetail = await _context.FoodDetail.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (foodDetail is null)
            throw new NotFoundException();

        _context.FoodDetail.Remove(foodDetail);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
