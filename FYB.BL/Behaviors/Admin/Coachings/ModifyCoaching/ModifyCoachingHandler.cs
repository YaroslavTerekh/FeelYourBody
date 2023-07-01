using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.ModifyCoaching;

public class ModifyCoachingHandler : IRequestHandler<ModifyCoachingCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public ModifyCoachingHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(ModifyCoachingCommand request, CancellationToken cancellationToken)
    {
        var coaching = await _context.Coachings.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if(coaching is null)
        {
            throw new NotFoundException(ErrorMessages.CoachingNotFound);
        }
        var oldPhotoId = coaching.CoachingPhotoId;

        coaching.Title = request.Title;
        coaching.Description = request.Description;
        coaching.Price = request.Price;
        coaching.CoachingPhotoId = request.CoachingPhotoId;
        coaching.CoachId = request.CoachId;
        coaching.FoodId = request.FoodId;

        await _context.SaveChangesAsync(cancellationToken);

        if (oldPhotoId != request.CoachingPhotoId) await _fileService.DeleteFileAsync(oldPhotoId, cancellationToken);

        throw new NotImplementedException();
    }
}
