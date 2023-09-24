﻿using FYB.BL.Behaviors.Admin;
using FYB.BL.Behaviors.Admin.Coaches.AddCoachDetails;
using FYB.BL.Behaviors.Admin.Coaches.AddNewCoach;
using FYB.BL.Behaviors.Admin.Coaches.DeleteCoach;
using FYB.BL.Behaviors.Admin.Coaches.DeleteCoachDetail;
using FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;
using FYB.BL.Behaviors.Admin.Coaches.ModifyCoachDetail;
using FYB.BL.Behaviors.Admin.Coachings.AddCoachingDetails;
using FYB.BL.Behaviors.Admin.Coachings.AddCoachingDetailsParent;
using FYB.BL.Behaviors.Admin.Coachings.AddPhotosToCoaching;
using FYB.BL.Behaviors.Admin.Coachings.ChangeDetailsParentTitle;
using FYB.BL.Behaviors.Admin.Coachings.CreateCoaching;
using FYB.BL.Behaviors.Admin.Coachings.DeleteCoaching;
using FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetail;
using FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetails;
using FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetailsParent;
using FYB.BL.Behaviors.Admin.Coachings.DeletePhotosFromCoaching;
using FYB.BL.Behaviors.Admin.Coachings.GetUsersInfo;
using FYB.BL.Behaviors.Admin.Coachings.ModifyCoaching;
using FYB.BL.Behaviors.Admin.Feedbacks.AddFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.AddPhotosToFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.DeleteFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.DeletePhotosFromFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.GetAllFeedbacks;
using FYB.BL.Behaviors.Admin.Feedbacks.GetFeedback;
using FYB.BL.Behaviors.Admin.Feedbacks.ModifyFeedback;
using FYB.BL.Behaviors.Admin.Foods.AddFoodPoint;
using FYB.BL.Behaviors.Admin.Foods.AddFoodPointParent;
using FYB.BL.Behaviors.Admin.Foods.CreateFood;
using FYB.BL.Behaviors.Admin.Foods.DeleteFood;
using FYB.BL.Behaviors.Admin.Foods.DeleteFoodPoint;
using FYB.BL.Behaviors.Admin.Foods.DeleteFoodPointParent;
using FYB.BL.Behaviors.Admin.Foods.GetFoodPointParents;
using FYB.BL.Behaviors.Admin.Foods.ModifyFood;
using FYB.BL.Behaviors.Admin.Foods.ModifyFoodPoint;
using FYB.BL.Behaviors.Admin.Foods.ModifyFoodPointParent;
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

    [HttpGet("users/all")]
    public async Task<IActionResult> GetAllUsersAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new GetUsersInfoQuery(), cancellationToken));
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
        [FromForm] UploadVideoModel command,
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        await _videoService.UploadVideoAsync(command.File, id, command.IsPreview, cancellationToken);

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

    [HttpPatch("coachings/examples/add")]
    public async Task<IActionResult> AddExamplesToCoachingAsync
    (
        [FromForm] AddPhotosToCoachingCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
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
    public async Task<IActionResult> DeleteCoachingDetailsAsync
    (
        [FromBody] DeleteCoachingDetailsCommand command,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("coaching/details/remove/{id:guid}")]
    public async Task<IActionResult> DeleteCoachingDetailAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        return Ok(await _mediatr.Send(new DeleteCoachingDetailCommand(id), cancellationToken));
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

    [HttpPost("coachings/details/parents/add")]
    public async Task<IActionResult> AddDetailParent
    (
        [FromBody] AddCoachingDetailsParentCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediatr.Send(command, cancellationToken));

    [HttpPatch("coachings/details/parents/title/change")]
    public async Task<IActionResult> ModifyDetailParent
    (
        [FromBody] ChangeDetailsParentTitleCommand command,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediatr.Send(command, cancellationToken));

    [HttpDelete("coachings/details/parents/{id:guid}/delete")]
    public async Task<IActionResult> DeleteDetailParent
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediatr.Send(new DeleteCoachingDetailsParentCommand(id), cancellationToken));

    [HttpPost("food/{id:guid}/point-parents/add")]
    public async Task<IActionResult> AddFoodPointParent
    (
        [FromRoute] Guid id,
        [FromBody] AddFoodPointParentCommand command,
        CancellationToken cancellationToken = default
    )
    {
        command.FoodId = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPut("food/{id:guid}/point-parents/change")]
    public async Task<IActionResult> ChangeFoodPointParent
    (
        [FromRoute] Guid id,
        [FromBody] ModifyFoodPointParentCommand command,
        CancellationToken cancellationToken = default
    )
    {
        command.FoodPointParentId = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("food/point-parents/{id:guid}")]
    public async Task<IActionResult> DeleteFoodPointParent
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediatr.Send(new DeleteFoodPointParentCommand(id), cancellationToken));

    [HttpGet("food/{id:guid}/point-parents")]
    public async Task<IActionResult> GetFoodPointParent
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediatr.Send(new GetFoodPointParentsQuery(id), cancellationToken));

    [HttpPost("coaches/{id:guid}/details/add")]
    public async Task<IActionResult> AddCoachDetailsAsync
    (
        [FromRoute] Guid id,
        [FromBody] AddCoachDetailsCommand command,
        CancellationToken cancellationToken = default
    )
    {
        command.CoachId = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpPut("coaches/details/{id:guid}/modify")]
    public async Task<IActionResult> ModfiyCoachDetailsAsync
    (
        [FromRoute] Guid id,
        [FromBody] ModifyCoachDetailCommand command,
        CancellationToken cancellationToken = default
    )
    {
        command.DetailId = id;

        return Ok(await _mediatr.Send(command, cancellationToken));
    }

    [HttpDelete("coaches/details/{id:guid}/delete")]
    public async Task<IActionResult> DeleteCoachDetailsAsync
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default
    ) => Ok(await _mediatr.Send(new DeleteCoachDetailCommand(id), cancellationToken));
}
