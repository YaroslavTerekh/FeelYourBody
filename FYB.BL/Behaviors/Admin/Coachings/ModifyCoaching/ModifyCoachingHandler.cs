using FYB.BL.Exceptions;
using FYB.BL.Services.Abstractions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
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
    private readonly IUnixService _unixService;

    public ModifyCoachingHandler(DataContext context, IFileService fileService, IUnixService unixService)
    {
        _context = context;
        _fileService = fileService;
        _unixService = unixService;
    }

    public async Task<Unit> Handle(ModifyCoachingCommand request, CancellationToken cancellationToken)
    {
        var coaching = await _context.Coachings
            .Include(t => t.CoachingPhoto)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if(coaching is null)
        {
            throw new NotFoundException(ErrorMessages.CoachingNotFound);
        }
        
        if(request.CoachingPhoto is not null)
        {
            var oldPhoto = await _context.Files.FirstOrDefaultAsync(t => t.Id == coaching.CoachingPhotoId, cancellationToken);
            oldPhoto.CoachingId = null;
        }

        coaching.Title = request.Title;
        coaching.Description = request.Description;
        coaching.Price = request.Price;
        coaching.CoachingPhoto = request.CoachingPhoto is not null ? await _fileService.UploadFileAsync(new AppFile { CoachingId = coaching.Id }, request.CoachingPhoto, cancellationToken) : coaching.CoachingPhoto;
        coaching.CoachId = request.CoachId;
        coaching.FoodId = request.FoodId;
        coaching.UnixExpireTime = request.AccessDays;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
