using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Exceptions;

public class RegisterException : Exception
{
    public HttpStatusCode Code { get; set; } = HttpStatusCode.BadRequest;

    public IEnumerable<IdentityError> Errors { get; set; }


    public RegisterException(HttpStatusCode code, IEnumerable<IdentityError> errors) : base()
    {
        Code = code;
        Errors = errors;
    }
}
