using FYB.BL.Exceptions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FYB.BL.Behaviors.Admin.Foods.ModifyFood;

public class ModifyFoodHandler : IRequestHandler<ModifyFoodCommand>
{
    private readonly DataContext _context;

    public ModifyFoodHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ModifyFoodCommand request, CancellationToken cancellationToken)
    {
        var food = await _context.Food.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (food is null) throw new NotFoundException(ErrorMessages.FoodNotFound);

        food.Title = request.Title;
        food.Description = request.Description;
        food.Price = request.Price;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
