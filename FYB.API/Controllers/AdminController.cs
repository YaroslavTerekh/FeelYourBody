using FYB.BL.Behaviors.Admin.Coaches.AddNewCoach;
using FYB.BL.Behaviors.Admin.Coaches.DeleteCoach;
using FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;
using FYB.BL.Behaviors.Admin.Coachings.AddPhotosToCoaching;
using FYB.BL.Behaviors.Admin.Coachings.CreateCoaching;
using FYB.BL.Behaviors.Admin.Coachings.DeleteCoaching;
using FYB.BL.Behaviors.Admin.Coachings.DeletePhotosFromCoaching;
using FYB.BL.Behaviors.Admin.Coachings.ModifyCoaching;
using FYB.BL.Behaviors.Admin.Foods.AddFoodPoint;
using FYB.BL.Behaviors.Admin.Foods.CreateFood;
using FYB.BL.Behaviors.Admin.Foods.DeleteFood;
using FYB.BL.Behaviors.Admin.Foods.DeleteFoodPoint;
using FYB.BL.Behaviors.Admin.Foods.ModifyFood;
using FYB.BL.Behaviors.Admin.Foods.ModifyFoodPoint;
using FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.CreateFAQ;
using FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.DeleteFAQ;
using FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.ModifyFAQ;
using FYB.BL.Services.Abstractions;
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
    private readonly IFileService _fileService;

    public AdminController(IMediator mediatr, IFileService fileService)
    {
        _mediatr = mediatr;
        _fileService = fileService;
    }

    [HttpPost("faq/create")]
    public async Task<IActionResult> CreateFAQAsync
    (
        [FromBody] CreateFAQCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPut("faq/modify/{id:guid}")]
    public async Task<IActionResult> ModifyFAQAsync
    (
        [FromBody] ModifyFAQCommand command,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        command.FAQId = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("faq/delete/{id:guid}")]
    public async Task<IActionResult> DeleteFAQAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new DeleteFAQCommand(id), cancellationToken));
    }

    [HttpPost("coaches/add")]
    public async Task<IActionResult> AddNewCoachAsync
    (
        [FromForm] AddNewCoachCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPut("coaches/modify/{id:guid}")]
    public async Task<IActionResult> ModifyCoachAsync
    (
        [FromForm] ModifyCoachCommand command,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        command.Id = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("coaches/delete/{id:guid}")]
    public async Task<IActionResult> DeleteCoachAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new DeleteCoachCommand(id), cancellationToken));
    }

    [HttpPost("coachings/add")]
    public async Task<IActionResult> CreateCoachingAsync
    (
        [FromForm] CreateCoachingCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPut("coachings/modify/{id:guid}")]
    public async Task<IActionResult> ModifyCoachingAsync
    (
        [FromRoute] Guid id,
        [FromForm] ModifyCoachingCommand command,
        CancellationToken cancellationToken = default
    )
    {
        command.Id = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPatch("coachings/{id:guid}/examples/add")]
    public async Task<IActionResult> AddExamplesToCoachingAsync
    (
        [FromRoute] Guid id,
        [FromForm] List<IFormFile> files,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new AddPhotosToCoachingCommand(id, files), cancellationToken));
    }

    [HttpDelete("coachings/{id:guid}/examples/delete")]
    public async Task<IActionResult> DeleteExamplesFromCoachingAsync
    (
        [FromRoute] Guid id,
        [FromBody] DeletePhotosFromCoachingCommand command,
        CancellationToken cancellationToken = default
    )
    {
        command.Id = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("coachings/delete/{id:guid}")]
    public async Task<IActionResult> DeleteCoachingAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new DeleteCoachingCommand(id), cancellationToken));
    }

    [HttpPost("file/add")]
    public async Task<IActionResult> AddNewFileAsync
    (
        IFormFile file,
        CancellationToken cancellationToken = default
    )
    {
        var appFile = await _fileService.UploadFileAsync(new Data.Entities.AppFile(), file, cancellationToken);

        return Ok(appFile.Id);
    }

    [HttpDelete("file/delete/{id:guid}")]
    public async Task<IActionResult> DeleteFileAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        await _fileService.DeleteFileAsync(id, cancellationToken);

        return Ok();
    }

    [HttpPost("food/create")]
    public async Task<IActionResult> CreateFoodAsync
    (
        [FromBody] CreateFoodCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPut("food/modify/{id:guid}")]
    public async Task<IActionResult> ModifyFoodAsync
    (
        [FromRoute] Guid id,
        [FromBody] ModifyFoodCommand command,
        CancellationToken cancellationToken = default
    )
    {
        command.Id = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("food/delete/{id:guid}")]
    public async Task<IActionResult> DeleteFoodAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new DeleteFoodCommand(id), cancellationToken));
    }

    [HttpPost("food/point/create")]
    public async Task<IActionResult> CreateFoodPointAsync
    (
        [FromBody] AddFoodPointCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPut("food/point/modify/{id:guid}")]
    public async Task<IActionResult> ModifyFoodPointAsync
    (
        [FromBody] ModifyFoodPointCommand command,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        command.Id = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("food/point/delete/{id:guid}")]
    public async Task<IActionResult> DeleteFoodPointAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new DeleteFoodPointCommand(id), cancellationToken));
    }
}
