using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.Entities;
using FYB.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Authentication.Login;

public class LoginHandler : IRequestHandler<LoginCommand, JWTResponse>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IJWTService _jwtService;

    public LoginHandler(
        SignInManager<User> signInManager, 
        UserManager<User> userManager, 
        IJWTService jwtService
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<JWTResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException(ErrorMessages.UserNotFound);

        if (!user.PhoneNumberConfirmed)
            throw new Exception(ErrorMessages.PhoneNumberIsNotConfirmed);

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if(!result.Succeeded)
            throw new Exception(ErrorMessages.WrongPassword);

        return _jwtService.GenerateJWT(user);
    }
}
