using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFoodPoint;

public class DeleteFoodPoint : IRequest
{
    public Guid Id { get; set; }

    public DeleteFoodPoint(Guid id) => Id = id;
}
