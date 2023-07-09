using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFoodPoint;

public class DeleteFoodPointCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteFoodPointCommand(Guid id) => Id = id;
}
