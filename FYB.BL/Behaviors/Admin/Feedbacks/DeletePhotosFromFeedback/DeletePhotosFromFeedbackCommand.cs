using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.DeletePhotosFromFeedback;

public class DeletePhotosFromFeedbackCommand : IRequest
{
    public Guid Id { get; set; }

    public List<Guid> PhotoIds { get; set; }
}
