using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.AddFoodPointParent;

public class AddFoodPointParentCommand : IRequest
{
    [JsonIgnore]
    public Guid FoodId { get; set; }

    public int DayNumber { get; set; }
}
