using FYB.Data.DbConnection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYB.Data.Entities;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.CreateFAQ;

public class CreateFAQHandler : IRequestHandler<CreateFAQCommand>
{
    private readonly DataContext _context;

    public CreateFAQHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateFAQCommand request, CancellationToken cancellationToken)
    {
        var faq = new FAQ
        {
            Question = request.Question,
            Answer = request.Answer
        };

        await _context.FAQs.AddAsync(faq, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
