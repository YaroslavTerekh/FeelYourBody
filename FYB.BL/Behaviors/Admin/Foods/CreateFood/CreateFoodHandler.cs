using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.CreateFood;

public class CreateFoodHandler : IRequestHandler<CreateFoodCommand>
{
    private readonly DataContext _context;

    public CreateFoodHandler(DataContext context)
    {
        _context =  context; ;
    }

    public async Task<Unit> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
    {
        var food = new Food
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            CoachingId = request.CoachingId
        };

        await _context.Food.AddAsync(food, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
