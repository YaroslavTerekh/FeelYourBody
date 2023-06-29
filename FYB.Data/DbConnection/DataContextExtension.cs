using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FYB.Data.DbConnection;
public static class DataContextExtension
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration builder)
    {
        services.AddDbContext<DataContext>(
                o => o.UseSqlServer(builder.GetConnectionString("DefaultConnection")));
        return services;
    }
}