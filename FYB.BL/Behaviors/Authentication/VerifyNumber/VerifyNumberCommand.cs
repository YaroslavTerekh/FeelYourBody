using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.VerifyNumber;

public class VerifyNumberCommand : IRequest<bool>
{
    public string PhoneNumber { get; set; }

    public long Code { get; set; }
}
