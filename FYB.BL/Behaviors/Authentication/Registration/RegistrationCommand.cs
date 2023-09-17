using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYB.Data.Entities;

namespace FYB.BL.Behaviors.Authentication.Registration;

public class RegistrationCommand : IRequest<User>
{
    public string FirstName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
