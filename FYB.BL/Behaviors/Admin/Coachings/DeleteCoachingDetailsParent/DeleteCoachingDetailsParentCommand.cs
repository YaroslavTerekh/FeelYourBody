using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetailsParent;

public class DeleteCoachingDetailsParentCommand : IRequest
{
    public Guid ParentId { get; set; }

    public DeleteCoachingDetailsParentCommand(Guid id) => ParentId = id;
}
