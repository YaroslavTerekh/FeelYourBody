using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.ModifyFAQ;

public class ModifyFAQCommand : IRequest
{
    [JsonIgnore]
    public Guid FAQId { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }
}
