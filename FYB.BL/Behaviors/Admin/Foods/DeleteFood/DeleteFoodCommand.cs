using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Foods.DeleteFood;

public class DeleteFoodCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteFoodCommand(Guid id) => Id = id;
}
