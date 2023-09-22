using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.AddCoachDetails;

public class AddCoachDetailsCommand : IRequest
{
    [JsonIgnore]
    public Guid CoachId { get; set; }

    public string Detail { get; set; }
}
