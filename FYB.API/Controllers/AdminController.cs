using FYB.BL.Behaviors.Admin.Coaches.AddNewCoach;
using FYB.BL.Behaviors.Admin.Coaches.DeleteCoach;
using FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;
using FYB.BL.Behaviors.Admin.Coachings.AddCoachingDetails;
using FYB.BL.Behaviors.Admin.Coachings.AddPhotosToCoaching;
using FYB.BL.Behaviors.Admin.Coachings.CreateCoaching;
using FYB.BL.Behaviors.Admin.Coachings.DeleteCoaching;
using FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetails;
using FYB.BL.Behaviors.Admin.Coachings.DeletePhotosFromCoaching;
using FYB.BL.Behaviors.Admin.Coachings.ModifyCoaching;
using FYB.BL.Behaviors.Admin.Feedbacks.AddFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.AddPhotosToFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.DeleteFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.DeletePhotosFromFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.GetAllFeedbacks;
using FYB.BL.Behaviors.Admin.Feedbacks.GetFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.ModifyFeedback;
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
using FYB.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FYB.API.Controllers;

[Route("api/admin")]
[ApiController]
[Authorize(Policy = Policies.Admin)]
public class AdminController : BaseController
{
    private readonly IMediator _mediatr;
    private readonly IFileService _fileService;
    private readonly IVideoService _videoService;

    public AdminController
    (
        IMediator mediatr,
        IFileService fileService,
        IVideoService videoService
    )
    {
        _mediatr = mediatr;
        _fileService = fileService;
        _videoService = videoService;
    }

    [HttpGet("coaching/feedback/{id:guid}")]
    public async Task<IActionResult> GetFeedbackAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetFeedbackCommand(id), cancellationToken));
    }

    [HttpGet("coaching/feedbacks/all")]
    public async Task<IActionResult> GetAllFeedbacksAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetAllFeedbacksQuery(), cancellationToken));
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

    [HttpPost("coachings/add")]
    public async Task<IActionResult> CreateCoachingAsync
    (
        [FromForm] CreateCoachingCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
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

    [HttpPost("file/add")]
    public async Task<IActionResult> AddNewFileAsync
    (
        IFormFile file,
        CancellationToken cancellationToken = default
    )
    {
        var appFile = await _fileService.UploadFileAsync(new AppFile(), file, cancellationToken);

        return Ok(appFile.Id);
    }

    [HttpPost("video/{id:guid}/add")]
    public async Task<IActionResult> AddNewVideoAsync
    (
        IFormFile file,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        await _videoService.UploadVideoAsync(file, id, cancellationToken);

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

    [HttpPost("food/point/create")]
    public async Task<IActionResult> CreateFoodPointAsync
    (
        [FromBody] AddFoodPointCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPost("coaching/feedback/add")]
    public async Task<IActionResult> AddFeedbackAsync
    (
        [FromForm] AddFeedbackCommand command,
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

    [HttpPut("coachings/modify")]
    public async Task<IActionResult> ModifyCoachingAsync
    (
        [FromForm] ModifyCoachingCommand command,
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

    [HttpPut("coaching/feedback/modify/{id:guid}")]
    public async Task<IActionResult> ModifyFeedbackAsync
    (
        [FromRoute] Guid id,
        [FromBody] ModifyFeedbackCommand command,
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

    [HttpPatch("coaching/details/add")]
    public async Task<IActionResult> AddCoachingDetailAsync
    (
        [FromBody] AddCoachingDetailsCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPatch("coaching/feedback/photos/add")]
    public async Task<IActionResult> AddPhotosToFeedbackAsync
    (
        [FromForm] AddPhotosToFeedbackCommand command,
        CancellationToken cancellationToken = default
    )
    {
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

    [HttpDelete("coaching/video/delete/{id:guid}")]
    public async Task<IActionResult> DeleteVideoAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        await _videoService.DeleteVideoAsync(id, cancellationToken);

        return Ok();
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

    [HttpDelete("coaching/details/remove")]
    public async Task<IActionResult> DeleteCoachingDetailAsync
    (
        [FromBody] DeleteCoachingDetailsCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
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

    [HttpDelete("food/delete/{id:guid}")]
    public async Task<IActionResult> DeleteFoodAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new DeleteFoodCommand(id), cancellationToken));
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

    [HttpDelete("coaching/feedback/delete/{id:guid}")]
    public async Task<IActionResult> DeleteFeedbackAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new DeleteFeedbackCommand(id), cancellationToken));
    }

    [HttpDelete("coaching/feedback/photos/delete")]
    public async Task<IActionResult> DeletePhotosFromFeedbackAsync
    (
        [FromBody] DeletePhotosFromFeedbackCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }
}
