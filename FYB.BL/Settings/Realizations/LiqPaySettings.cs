using FYB.BL.Settings.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Settings.Realizations;

public class LiqPaySettings : ILiqPaySettings
{
    public LiqPaySettings(IConfiguration configuration)
    {
        var appsettingSection = configuration.GetSection("LiqPaySettings");

        PublicKey = appsettingSection.GetValue<string>("PublicKey");
        PrivateKey = appsettingSection.GetValue<string>("PrivateKey");
        ServerUrl = appsettingSection.GetValue<string>("ServerUrl");
    }

    public string PublicKey { get; }
    public string PrivateKey { get; }
    public string ServerUrl { get; }
}
