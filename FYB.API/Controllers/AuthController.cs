using FYB.BL.Behaviors.Authentication.GetUser;
using FYB.BL.Behaviors.Authentication.Login;
using FYB.BL.Behaviors.Authentication.Registration;
using FYB.BL.Behaviors.Authentication.SendVerificationCode;
using FYB.BL.Behaviors.Authentication.VerifyNumber;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYB.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : BaseController
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

    [HttpPost("generate-verify-code")]
    public async Task<IActionResult> GenerateVerifyCodeAsync
    (
        [FromBody] SendVerificationCodeCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPost("verify-code")]
    public async Task<IActionResult> VerifyCodeAsync
    (
        [FromBody] VerifyNumberCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [Authorize]
    [HttpGet("get-user")]
    public async Task<IActionResult> GetUserAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetUserQuery(CurrentUserId), cancellationToken));
    }
}
