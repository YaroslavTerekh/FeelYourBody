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

namespace FYB.BL.Behaviors.Admin.Coaches.ModifyCoach;

public class ModifyCoachHandler : IRequestHandler<ModifyCoachCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public ModifyCoachHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(ModifyCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = await _context.Coaches.Include(t => t.Coachings).FirstOrDefaultAsync(t => t.Id == request.Id);
        var oldAvatarId = coach.AvatarId;

        if(coach is null)
        {
            throw new NotFoundException(ErrorMessages.CoachNotFound);
        }

        var newAvatar = await _context.Files.FirstOrDefaultAsync(t => t.Id == request.AvatarId, cancellationToken);

        if(newAvatar is null)
        {
            throw new NotFoundException(ErrorMessages.FileNotFound);
        }

        coach.AvatarId = coach.AvatarId != request.AvatarId ? request.AvatarId : coach.AvatarId;
        coach.FirstName = request.FirstName;
        coach.LastName = request.LastName;
        coach.Description = request.Description;
        coach.Coachings = await _context.Coachings.Where(t => request.CoachingIds.Contains(t.Id)).ToListAsync(cancellationToken);
        coach.InstagramLink = request.InstagramLink;
        coach.BirthDate = request.BirthDate;

        await _context.SaveChangesAsync(cancellationToken);

        if (oldAvatarId != request.AvatarId)
        {
            await _fileService.DeleteFileAsync(oldAvatarId, cancellationToken);
        }

        return Unit.Value;
    }
}
