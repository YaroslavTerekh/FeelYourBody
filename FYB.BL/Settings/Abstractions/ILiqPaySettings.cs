using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Settings.Abstractions;

public interface ILiqPaySettings
{
    public string PublicKey { get; }

    public string PrivateKey { get; }

    public string ServerUrl { get; }
}
