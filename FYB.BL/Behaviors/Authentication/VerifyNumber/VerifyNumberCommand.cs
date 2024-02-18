using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYB.Data.Models;

namespace FYB.BL.Behaviors.Authentication.VerifyNumber;

public class VerifyNumberCommand : IRequest<JWTResponse>
{
    public string PhoneNumber { get; set; }

    public long Code { get; set; }
}
