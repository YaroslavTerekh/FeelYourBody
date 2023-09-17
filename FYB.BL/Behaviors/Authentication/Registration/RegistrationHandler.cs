﻿
using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.BL.Settings.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FYB.BL.Behaviors.Authentication.Registration;

public class RegistrationHandler : IRequestHandler<RegistrationCommand, User>
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IPhoneNumberService _phoneNumberService;

    public RegistrationHandler(DataContext context, UserManager<User> userManager, IPhoneNumberService phoneNumberService)
    {
        _context = context;
        _userManager = userManager;
        _phoneNumberService = phoneNumberService;
    }

    public async Task<User> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.FirstName,
            Email = request.Email,
            PhoneNumber = _phoneNumberService.FormatPhoneNumber(request.PhoneNumber),
            TemporaryCode = null
        };

        if(await _context.Users.AnyAsync(t => t.PhoneNumber == _phoneNumberService.FormatPhoneNumber(newUser.PhoneNumber), cancellationToken))
        {
            throw new RegisterException(HttpStatusCode.BadRequest, new IdentityError[] { new IdentityError { Code = HttpStatusCode.BadRequest.ToString(), Description = ErrorMessages.UserWithNumberExists } });
        }

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if(!result.Succeeded)
        {
            throw new RegisterException(HttpStatusCode.BadRequest, result.Errors);
        }
        
        return new User
        {
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.FirstName,
            Email = request.Email,
            PhoneNumber = _phoneNumberService.FormatPhoneNumber(request.PhoneNumber),
            TemporaryCode = null,
        };
    }
}
