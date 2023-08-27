using FYB.BL.Settings.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Settings.Realizations;

public class TwilioSettings : ITwilioSettings
{
    public TwilioSettings(IConfiguration configuration)
    {
        var appsettingSection = configuration.GetSection("TwilioSettings");

        AccountSid = appsettingSection.GetRequiredSection("AccountSid").Value;
        FromPhoneNumber = appsettingSection.GetRequiredSection("FromPhoneNumber").Value;
        AuthToken = appsettingSection.GetRequiredSection("AuthToken").Value;
    }

    public string AccountSid { get; }

    public string AuthToken { get; }

    public string FromPhoneNumber { get; }
}
