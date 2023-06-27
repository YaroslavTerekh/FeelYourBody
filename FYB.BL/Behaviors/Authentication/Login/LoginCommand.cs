using FYB.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.Login;

public class LoginCommand : IRequest<JWTResponse>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
