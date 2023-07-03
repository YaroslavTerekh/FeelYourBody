using FYB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Abstractions;

public interface IProductService<T>
{
    public Task AddUserToProduct(Guid userId, Guid productId, CancellationToken cancellationToken);

    public Task DeleteUserFromProduct(Guid userId, Guid productId, CancellationToken cancellationToken);
}
