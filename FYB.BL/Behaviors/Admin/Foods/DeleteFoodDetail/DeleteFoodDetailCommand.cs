using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFoodDetail;

public class DeleteFoodDetailCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteFoodDetailCommand(Guid id) => Id = id;
}
