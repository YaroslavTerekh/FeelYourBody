using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.ChangeDetailsParentTitle;

public class ChangeDetailsParentTitleCommand : IRequest
{
    public Guid ParentId { get; set; }

    public string Title { get; set; }
}
