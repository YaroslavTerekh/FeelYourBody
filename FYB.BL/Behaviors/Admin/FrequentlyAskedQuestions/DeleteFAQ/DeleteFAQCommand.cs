using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.DeleteFAQ;

public class DeleteFAQCommand : IRequest
{
    [JsonIgnore]
    public Guid FAQId { get; set; }

    public DeleteFAQCommand(Guid id) => FAQId = id;
}
