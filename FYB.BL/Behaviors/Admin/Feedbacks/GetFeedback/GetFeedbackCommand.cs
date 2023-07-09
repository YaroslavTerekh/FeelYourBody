using FYB.Data.Common.DataTransferObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.GetFeedback;

public class GetFeedbackCommand : IRequest<FeedbackDTO>
{
    public Guid Id { get; set; }

    public GetFeedbackCommand(Guid id) => Id = id;
}
