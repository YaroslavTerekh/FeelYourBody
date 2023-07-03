using FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;
using FYB.BL.Behaviors.Coaches.GetAllCoaches;
using FYB.BL.Behaviors.Coachings.GetAllCoachings;
using FYB.BL.Behaviors.Coachings.GetCoaching;
using FYB.BL.Behaviors.Foods.GetAllFood;
using FYB.BL.Behaviors.Foods.GetFood;
using FYB.BL.Behaviors.FrequentlyAskedQuestions.GetAllFAQ;
using FYB.BL.Services.Abstractions;
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYB.API.Controllers;

[Route("api/content")]
[ApiController]
public class ContentController : BaseController
{
    private readonly IMediator _mediatr;
    private readonly IProductService<Coaching> _productService;
    public ContentController(IMediator mediatr, IProductService<Coaching> productService)
    {
        _mediatr = mediatr;
        _productService = productService;
    }

    [HttpGet("coaches")]
    public async Task<IActionResult> GetAllCoachesAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetAllCoachesQuery(), cancellationToken));
    }

    [HttpGet("faq")]
    public async Task<IActionResult> GetAllFAQAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetAllFAQQuery(), cancellationToken));
    }

    [HttpGet("coachings/all")]
    public async Task<IActionResult> GetAllCoachingsAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetAllCoachingsQuery(), cancellationToken));
    }

    [Authorize]
    [HttpGet("coachings/{id:guid}")]
    public async Task<IActionResult> GetCoachingAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetCoachingQuery(id, CurrentUserId), cancellationToken));
    }

    [HttpGet("food/all")]
    public async Task<IActionResult> GetAllFoodAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetAllFoodQuery(), cancellationToken));
    }

    [Authorize]
    [HttpGet("food/{id:guid}")]
    public async Task<IActionResult> GetFoodAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetFoodQuery(id, CurrentUserId), cancellationToken));
    }

    [HttpPost("TEST")]
    public async Task<IActionResult> Test()
    {
        await _productService.AddUserToProduct(Guid.Parse("28021FE8-69B0-4E99-9696-08DB78858870"), Guid.Parse("2DF24136-703B-4547-A687-044761C94F1B"), default);

        return Ok();
    }
}
