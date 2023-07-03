using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.ModifyFoodPoint;

public class ModifyFoodPointCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public long PortionMass { get; set; }
}
