using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coaches.ModifyCoachDetail;

public class ModifyCoachDetailCommand : IRequest
{
    [JsonIgnore]
    public Guid DetailId { get; set; }

    public string Detail { get; set; }
}
