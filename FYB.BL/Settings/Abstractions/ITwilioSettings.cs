using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Settings.Abstractions;

public interface ITwilioSettings
{
    public string AccountSid { get; }

    public string AuthToken { get; }

    public string FromPhoneNumber { get; }
}
