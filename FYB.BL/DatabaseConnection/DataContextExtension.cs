using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FYB.BL.DatabaseConnection;

public static class DataContextExtension
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration builder)
    {
        services.AddDbContext<DataContext>(
                o => o.UseSqlServer(builder.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("FYB.API")));
        return services;
    }
}
