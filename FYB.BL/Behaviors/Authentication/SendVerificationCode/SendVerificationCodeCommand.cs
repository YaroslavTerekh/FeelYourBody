using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.SendVerificationCode;

public class SendVerificationCodeCommand : IRequest
{
    public string PhoneNumber { get; set; }
}