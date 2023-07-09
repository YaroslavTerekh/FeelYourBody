using FYB.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.FrequentlyAskedQuestions.GetAllFAQ;

public class GetAllFAQQuery : IRequest<List<FAQ>>
{
}
