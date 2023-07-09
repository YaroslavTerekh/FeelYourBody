using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.AddPhotosToCoaching;

public class AddPhotosToCoachingCommand : IRequest
{
    public Guid Id { get; set; }

    public List<IFormFile> Photos { get; set; }

    public AddPhotosToCoachingCommand(Guid id, List<IFormFile> photos)
    {
        Id = id;
        Photos = photos;
    }
}
