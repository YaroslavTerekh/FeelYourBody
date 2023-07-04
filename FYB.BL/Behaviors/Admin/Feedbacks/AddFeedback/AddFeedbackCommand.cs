using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.AddFeedback;

public class AddFeedbackCommand : IRequest
{
    public string FeedbackText { get; set; }

    public string InstagramLink { get; set; }

    public Guid CoachingId { get; set; }

    public List<IFormFile> Photos { get; set; } = new();
}
