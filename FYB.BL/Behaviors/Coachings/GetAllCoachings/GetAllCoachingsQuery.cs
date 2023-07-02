using FYB.Data.Common.DataTransferObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Coachings.GetAllCoachings;

public class GetAllCoachingsQuery : IRequest<List<CoachingDTO>>
{
}
