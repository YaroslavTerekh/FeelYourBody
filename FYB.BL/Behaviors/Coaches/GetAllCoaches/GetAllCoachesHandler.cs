using FYB.BL.Settings.Realizations;
using FYB.Data.Common.DataTransferObjects;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYB.BL.Exceptions;
using FYB.Data.Constants;

namespace FYB.BL.Behaviors.Coaches.GetAllCoaches;

public class GetAllCoachesHandler : IRequestHandler<GetAllCoachesQuery, List<CoachDTO>>
{
    private readonly DataContext _context;
    private readonly HostSettings _hostSettings;

    public GetAllCoachesHandler(DataContext context, HostSettings hostSettings)
    {
        _context = context;
        _hostSettings = hostSettings;
    }

    public async Task<List<CoachDTO>> Handle(GetAllCoachesQuery request, CancellationToken cancellationToken)
    {
        var coaches = await _context.Coaches
            .Include(t => t.Photos)
            .Select(t => new CoachDTO
            {
                FirstName = t.FirstName,
                LastName = t.LastName,
                InstagramLink = t.InstagramLink,
                Description = t.Description,
                Details = t.CoachDetails.Select(d => new CoachDetailDTO
                {
                    Id = d.Id,
                    Detail = d.Detail,
                    CreatedDate = d.CreatedDate
                }).ToList(),
                Photos = t.Photos.Select(p => new AppFileDTO
                {
                    Id = p.Id,
                    FileName = p.FileName,
                    FileExtension = p.FileExtension,
                    FilePath = String.Concat(_hostSettings.ApplicationUrl, p.FilePath.Replace(@"\", "/"))
                }).ToList(),
                Id = t.Id,
                BirthDate = t.BirthDate
            })
            .ToListAsync(cancellationToken);

        return coaches;
    }
}
