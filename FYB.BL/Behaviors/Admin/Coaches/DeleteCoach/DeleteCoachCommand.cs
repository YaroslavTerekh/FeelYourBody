using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.DeleteCoach;

public class DeleteCoachCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public DeleteCoachCommand(Guid id) => Id = id;
}
