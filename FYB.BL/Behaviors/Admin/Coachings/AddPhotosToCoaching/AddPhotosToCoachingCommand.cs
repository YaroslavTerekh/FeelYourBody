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

    public List<Photo> Photos { get; set; }
}

public class Photo
{
    public IFormFile PhotoFile { get; set; }

    public int OrderId { get; set; }
}
