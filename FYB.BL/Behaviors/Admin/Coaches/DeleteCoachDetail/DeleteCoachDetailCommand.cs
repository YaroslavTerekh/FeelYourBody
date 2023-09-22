using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.DeleteCoachDetail;

public class DeleteCoachDetailCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteCoachDetailCommand(Guid id)
    {
        Id = id;
    }
}
