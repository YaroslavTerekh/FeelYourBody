using FYB.Data.Common.DataTransferObjects;
using FYB.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Coachings.GetCoaching;

public class GetCoachingQuery : IRequest<CoachingDTO>
{
    public Guid Id { get; set; }

    public Guid CurrentUserId { get; set; }

    public GetCoachingQuery(Guid id, Guid currentUserId)
    {
        Id = id;
        CurrentUserId = currentUserId;
    }
}
