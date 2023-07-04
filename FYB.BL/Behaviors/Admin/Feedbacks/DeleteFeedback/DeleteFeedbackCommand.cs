using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.DeleteFeedback;

public class DeleteFeedbackCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteFeedbackCommand(Guid id) => Id = id;
}
