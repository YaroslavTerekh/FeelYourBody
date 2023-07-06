using FYB.BL.Exceptions;
using FYB.Data.Common;
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

namespace FYB.BL.Behaviors.Videos.GetVideo;

public class GetVideoHandler : IRequestHandler<GetVideoQuery, CoachingVideo>
{
    private readonly DataContext _context;

    public GetVideoHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<CoachingVideo> Handle(GetVideoQuery request, CancellationToken cancellationToken)
    {
        var video = await _context.CoachingVideos
            .Include(t => t.Coaching)
                .ThenInclude(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (video is null) throw new NotFoundException(ErrorMessages.CoachingVideoNotFound);

        var currentUser = await _context.Users.FirstOrDefaultAsync(t => t.Id == request.CurrentUserId, cancellationToken);

        if (currentUser is null) throw new NotFoundException(ErrorMessages.UserNotFound);

        if (!video.Coaching.Users.Contains(currentUser) && currentUser.Role != Role.Admin) throw new Exception(ErrorMessages.ContentAccessForbidden);

        return video;
    }
}
