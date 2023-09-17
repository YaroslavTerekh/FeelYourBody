using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Abstractions;

public interface IPhoneNumberService
{
    public string FormatPhoneNumber(string phoneNumber);
}
