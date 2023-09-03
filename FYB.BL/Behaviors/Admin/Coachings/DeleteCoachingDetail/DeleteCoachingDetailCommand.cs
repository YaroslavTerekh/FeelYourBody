using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetail;

public class DeleteCoachingDetailCommand : IRequest
{
    public Guid DetailId { get; set; }

    public DeleteCoachingDetailCommand(Guid id) => DetailId = id;
}
