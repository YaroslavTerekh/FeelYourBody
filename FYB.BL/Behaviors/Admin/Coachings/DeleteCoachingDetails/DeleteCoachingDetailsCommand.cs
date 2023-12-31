﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Coachings.DeleteCoachingDetails;

public class DeleteCoachingDetailsCommand : IRequest
{
    public List<Guid> Ids { get; set; }
}
