using FYB.Data.Common.DataTransferObjects;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Coaches.GetAllCoaches;

public class GetAllCoachesHandler : IRequestHandler<GetAllCoachesQuery, List<CoachDTO>>
{
    private readonly DataContext _context;

    public GetAllCoachesHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<CoachDTO>> Handle(GetAllCoachesQuery request, CancellationToken cancellationToken)
    {
        var coaches = await _context.Coaches
            .Include(t => t.Avatar)
            .Select(t => new CoachDTO
            {
                FirstName = t.FirstName,
                LastName = t.LastName,
                InstagramLink = t.InstagramLink,
                Description = t.Description,
                Avatar = new AppFileDTO
                {
                    Id = t.Avatar.Id,
                    FileName = t.Avatar.FileName,
                    FileExtension = t.Avatar.FileExtension
                },
                Id = t.Id,
                BirthDate = t.BirthDate
            })
            .ToListAsync(cancellationToken);

        return coaches;
    }
}
