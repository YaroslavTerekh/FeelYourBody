using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.CreateFAQ;

public class CreateFAQCommand : IRequest
{
    public string Question { get; set; }

    public string Answer { get; set; }
}
