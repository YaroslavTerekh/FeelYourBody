using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.DeleteCoaching;

public class DeleteCoachingCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public DeleteCoachingCommand(Guid id) => Id = id;
}
