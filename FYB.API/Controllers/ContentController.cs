using FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;
using FYB.BL.Behaviors.Coaches.GetAllCoaches;
using FYB.BL.Behaviors.Coachings.GetAllCoachings;
using FYB.BL.Behaviors.Coachings.GetCoaching;
using FYB.BL.Behaviors.Files.GetFile;
using FYB.BL.Behaviors.Foods.GetAllFood;
using FYB.BL.Behaviors.Foods.GetFood;
using FYB.BL.Behaviors.FrequentlyAskedQuestions.GetAllFAQ;
using FYB.BL.Services.Abstractions;
using FYB.Data.Common;
using FYB.Data.Constants;
using FYB.Data.Entities;
using LiqPay.SDK.Dto;
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
    private readonly IWebHostEnvironment _env;
    private readonly ILiqPayService _liqpayService;

    public ContentController
    (
        IMediator mediatr, 
        IProductService<Coaching> productService,
        IWebHostEnvironment env,
        ILiqPayService liqpayService
    )
    {
        _mediatr = mediatr;
        _productService = productService;
        _env = env;
        _liqpayService = liqpayService;
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

    [HttpGet("file/{id:guid}")]
    public async Task<IActionResult> GetFileAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        var file = await _mediatr.Send(new GetFileQuery(id), cancellationToken);

        return PhysicalFile(Path.Combine(_env.ContentRootPath, file.FilePath), file.FileExtension);
    }

    [Authorize(Policy = Policies.Users)]
    [HttpGet("generate-pay-form/{id:guid}")]
    public async Task<IActionResult> GenerateForm(
        [FromRoute] Guid id,
        [FromQuery] PurchaseProductType productType,
        CancellationToken cancellationToken = default)
    {
        var result = await _liqpayService.GenerateForm(id, CurrentUserId, productType, cancellationToken);
        return Ok(result);
    }

    [HttpPost("process-pay-response")]
    public async Task<IActionResult> ProcessPayResponseAsync(
        [FromForm] Dictionary<string, string> formData, 
        CancellationToken cancellationToken = default
    )
    {
        await _liqpayService.ProcessCallbackAsync(formData, cancellationToken);
        return Ok();
    }
}