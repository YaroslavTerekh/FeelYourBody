using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Realizations;

public class FoodProductService : IProductService<Food>
{
    private readonly DataContext _context;

    public FoodProductService(DataContext context)
    {
        _context = context;
    }

    public async Task AddUserToProduct(Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        var product = await _context.Food
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == productId, cancellationToken);

        if (product is null) throw new NotFoundException(ErrorMessages.FoodNotFound);

        var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == userId, cancellationToken);

        if (user is null) throw new NotFoundException(ErrorMessages.UserNotFound);
        if (product.Users.Contains(user)) throw new Exception(ErrorMessages.ProductAlreadyBought);

        product.Users.Add(user);
    }

    public async Task DeleteUserFromProduct(Guid userId, Guid productId, CancellationToken cancellationToken)
    {
        var product = await _context.Food
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == productId, cancellationToken);

        if (product is null) throw new NotFoundException(ErrorMessages.FoodNotFound);

        var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == userId, cancellationToken);

        if (user is null) throw new NotFoundException(ErrorMessages.UserNotFound);

        product.Users.Remove(user);
    }
}
