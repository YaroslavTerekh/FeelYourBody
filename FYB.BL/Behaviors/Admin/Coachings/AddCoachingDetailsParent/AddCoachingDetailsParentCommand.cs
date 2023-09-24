using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.AddCoachingDetailsParent;

public class AddCoachingDetailsParentCommand : IRequest<Guid>
{
    public Guid CoachingId { get; set; }

    public string Title { get; set; }
}
