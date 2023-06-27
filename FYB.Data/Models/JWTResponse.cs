using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.Data.Models;

public class JWTResponse
{
    public string Token { get; set; }

    public DateTime ExpireDate { get; set; }
}
