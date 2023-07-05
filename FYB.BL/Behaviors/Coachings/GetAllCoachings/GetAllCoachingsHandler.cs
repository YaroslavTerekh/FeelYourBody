using AutoMapper;
using FYB.Data.Common.DataTransferObjects;
using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Coachings.GetAllCoachings;

public class GetAllCoachingsHandler : IRequestHandler<GetAllCoachingsQuery, List<CoachingDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetAllCoachingsHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CoachingDTO>> Handle(GetAllCoachingsQuery request, CancellationToken cancellationToken)
    {
        var coachings = await _context.Coachings
            .Include(t => t.Coach)
            .Include(t => t.CoachingPhoto)
            .Include(t => t.ExamplePhotos)
            .Include(t => t.Food)
                .ThenInclude(t => t.FoodPoints)
            .Include(t => t.Feedbacks)
            .Include(t => t.CoachingDetails)
            .Select(t => _mapper.Map<Coaching, CoachingDTO>(t))
            .ToListAsync(cancellationToken);

        return coachings;
    }
}
