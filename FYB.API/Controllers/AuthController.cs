﻿using FYB.BL.Behaviors.Authentication.Login;
using FYB.BL.Behaviors.Authentication.Registration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYB.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediatr;

    public AuthController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync
    (
        [FromBody] RegistrationCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync
    (
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }
}
