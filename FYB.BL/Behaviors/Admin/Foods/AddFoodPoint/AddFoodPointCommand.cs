using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.AddFoodPoint;

public class AddFoodPointCommand : IRequest
{
    public string Title { get; set; }

    public string Description { get; set; }

    public long PortionMass { get; set; }

    public string CoockingMethod { get; set; }

    public Guid FoodPointParentId { get; set; }
}
