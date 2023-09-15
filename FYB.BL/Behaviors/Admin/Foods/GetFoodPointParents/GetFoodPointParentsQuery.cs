using FYB.Data.Common.DataTransferObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.GetFoodPointParents;

public class GetFoodPointParentsQuery : IRequest<List<FoodPointsParentDTO>>
{
    public Guid FoodID { get; set; }

    public GetFoodPointParentsQuery(Guid id)
    {
        FoodID = id;
    }
}
