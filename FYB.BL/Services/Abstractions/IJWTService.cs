using FYB.Data.Entities;
using FYB.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Abstractions;

public interface IJWTService
{
    public JWTResponse GenerateJWT(User user);
}
