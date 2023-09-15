using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.ModifyFoodPointParent;

public class ModifyFoodPointParentCommand : IRequest
{
    [JsonIgnore]
    public Guid FoodPointParentId { get; set; }

    public int DayNumber { get; set; }
}
