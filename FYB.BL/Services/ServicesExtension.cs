using FYB.BL.Services.Abstractions;
using FYB.BL.Services.Realizations;
using FYB.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services;

public static class ServicesExtension
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<IJWTService, JWTService>();
        services.AddTransient<IUnixService, UnixService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IVideoService, VideoService>();
        services.AddTransient<ILiqPayService, LiqPayService>();        
        services.AddTransient<IProductService<Food>, FoodProductService>();
        services.AddTransient<IProductService<Coaching>, CoachingProductService>();

        return services;
    }
}
