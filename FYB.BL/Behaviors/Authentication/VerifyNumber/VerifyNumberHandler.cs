using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYB.Data.Models;

namespace FYB.BL.Behaviors.Authentication.VerifyNumber;

public class VerifyNumberHandler : IRequestHandler<VerifyNumberCommand, JWTResponse>
{
    private readonly DataContext _context;
    private readonly IPhoneNumberService _phoneNumberService;
    private readonly IJWTService _jwtService;

    public VerifyNumberHandler(DataContext context, IPhoneNumberService phoneNumberService, IJWTService jwtService)
    {
        _context = context;
        _phoneNumberService = phoneNumberService;
        _jwtService = jwtService;
    }

    public async Task<JWTResponse> Handle(VerifyNumberCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.PhoneNumber == _phoneNumberService.FormatPhoneNumber(request.PhoneNumber), cancellationToken);

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (user.PhoneNumberConfirmed) 
            throw new Exception(ErrorMessages.PhoneNumberAlreadyConfirmed);

        if (user.TemporaryCode != request.Code) throw new Exception(ErrorMessages.CodeNotValid);;

        user.PhoneNumberConfirmed = true;
        await _context.SaveChangesAsync(cancellationToken);

        return _jwtService.GenerateJWT(user);
    }
}
