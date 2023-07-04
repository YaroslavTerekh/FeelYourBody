using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.ModifyFeedback;

public class ModifyFeedbackCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string FeedbackText { get; set; }

    public string InstagramLink { get; set; }
}
