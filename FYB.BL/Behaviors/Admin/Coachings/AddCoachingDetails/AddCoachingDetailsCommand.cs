using FYB.Data.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.AddCoachingDetails;

public class AddCoachingDetailsCommand : IRequest
{
    public Guid Id { get; set; }

    public List<DetailModel> CoachingDetails { get; set; }
}

public class DetailModel
{
    public CoachingDetailtsIconEnum Icon { get; set; }

    public string Detail { get; set; }
}