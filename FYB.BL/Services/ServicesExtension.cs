using FYB.BL.Services.Abstractions;
using FYB.BL.Services.Realizations;
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

        return services;
    }
}
