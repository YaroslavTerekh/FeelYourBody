using FYB.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Videos.GetVideo;

public class GetVideoQuery : IRequest<CoachingVideo>
{   
    public Guid CurrentUserId { get; set; }

    public Guid Id { get; set; }

    public GetVideoQuery(Guid currentUserId, Guid id)
    {
        CurrentUserId = currentUserId;
        Id = id;
    }
}
