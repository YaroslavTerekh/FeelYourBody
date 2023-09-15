using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFoodPointParent;

public class DeleteFoodPointParentCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteFoodPointParentCommand(Guid id) => Id = id; 
}
