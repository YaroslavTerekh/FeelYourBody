using FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.CreateFAQ;
using FYB.Data.Common;
using FYB.Data.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYB.API.Controllers;

[Route("api/admin")]
[ApiController]
[Authorize(Policy = Policies.Admin)]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediatr;

    public AdminController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFAQAsync
    (
        [FromBody] CreateFAQCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }
}
