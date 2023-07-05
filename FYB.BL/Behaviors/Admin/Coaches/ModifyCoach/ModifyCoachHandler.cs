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

        if(coach is null)
        {
            throw new NotFoundException(ErrorMessages.CoachNotFound);
        }

        if(request.Avatar is not null)
        {
            var oldAvatar = await _context.Files.FirstOrDefaultAsync(t => t.Id == coach.AvatarId, cancellationToken);

            oldAvatar.CoachId = null;
        }

        coach.Avatar = request.Avatar is null ? coach.Avatar : await _fileService.UploadFileAsync(new AppFile { CoachId = coach.Id }, request.Avatar, cancellationToken);
        coach.FirstName = request.FirstName;
        coach.LastName = request.LastName;
        coach.Description = request.Description;
        coach.InstagramLink = request.InstagramLink;
        coach.BirthDate = request.BirthDate;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
