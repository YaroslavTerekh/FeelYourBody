using FYB.BL.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Realizations;

public class UnixService : IUnixService
{
    public DateTime GenerateExpireDate(long unix)
    {
        DateTime resultDate = DateTime.UnixEpoch.AddSeconds(unix).ToLocalTime(); ;

        return resultDate;
    }

    public long GenerateUnix(long days)
    {
        DateTime endDate = DateTime.UtcNow.AddDays(days);

        return ((DateTimeOffset)endDate).ToUnixTimeSeconds();
    }
}
