using FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;
using FYB.BL.Behaviors.Coaches.GetAllCoaches;
using FYB.BL.Behaviors.Coachings.GetAllCoachings;
using FYB.BL.Behaviors.FrequentlyAskedQuestions.GetAllFAQ;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYB.API.Controllers;

[Route("api/content")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly IMediator _mediatr;

    public ContentController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("get/coaches")]
    public async Task<IActionResult> GetAllCoachesAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetAllCoachesQuery(), cancellationToken));
    }

    [HttpGet("get/faq")]
    public async Task<IActionResult> GetAllFAQAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetAllFAQQuery(), cancellationToken));
    }

    [HttpGet("get/coachings/all")]
    public async Task<IActionResult> GetAllCoachingsAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetAllCoachingsQuery(), cancellationToken));
    } 

    [HttpPost("TEST")]
    public IActionResult Test([FromForm] List<IFormFile> file)
    {
        return Ok();
    }
}
