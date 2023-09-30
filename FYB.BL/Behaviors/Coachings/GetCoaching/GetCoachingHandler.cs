using AutoMapper;
using FYB.BL.Exceptions;
using FYB.Data.Common;
using FYB.Data.Common.DataTransferObjects;
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

namespace FYB.BL.Behaviors.Coachings.GetCoaching;

public class GetCoachingHandler : IRequestHandler<GetCoachingQuery, CoachingDTO>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetCoachingHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CoachingDTO> Handle(GetCoachingQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == request.CurrentUserId, cancellationToken);

        if (user is null) throw new NotFoundException(ErrorMessages.UserNotFound);

        var coaching = await _context.Coachings
            .Include(t => t.Coach)
            .Include(t => t.CoachingPhoto)
            .Include(t => t.ExamplePhotos)
            .Include(t => t.Food)
            .ThenInclude(t => t.FoodPoints)
            .Include(t => t.Feedbacks)
            .Include(t => t.Videos)
            .Include(t => t.CoachingDetailParents)
                .ThenInclude(t => t.Details)
            .Include(t => t.Users)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (coaching is null) throw new NotFoundException(ErrorMessages.CoachingNotFound);
        if (!coaching.Users.Contains(user) && user.Role != Role.Admin) throw new Exception(ErrorMessages.ContentAccessForbidden);

        return _mapper.Map<Coaching, CoachingDTO>(coaching);
    }
}
