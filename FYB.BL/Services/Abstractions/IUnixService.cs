using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Abstractions;

public interface IUnixService
{
    public long GenerateUnix(long days);

    public DateTime GenerateExpireDate(long unix);
}
