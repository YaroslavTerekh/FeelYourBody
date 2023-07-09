
using FYB.BL.Exceptions;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.Registration;

public class RegistrationHandler : IRequestHandler<RegistrationCommand>
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;

    public RegistrationHandler(DataContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if(!result.Succeeded)
        {
            throw new RegisterException(HttpStatusCode.BadRequest, result.Errors);
        }

        return Unit.Value;
    }
}
