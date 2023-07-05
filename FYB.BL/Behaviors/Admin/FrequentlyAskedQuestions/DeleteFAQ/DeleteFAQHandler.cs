using FYB.BL.Exceptions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.DeleteFAQ;

public class DeleteFAQHandler : IRequestHandler<DeleteFAQCommand>
{
    private readonly DataContext _context;

    public DeleteFAQHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFAQCommand request, CancellationToken cancellationToken)
    {
        var faq = await _context.FAQs.FirstOrDefaultAsync(t => t.Id == request.FAQId, cancellationToken);

        if (faq is null)
        {
            throw new NotFoundException(ErrorMessages.FAQNotFound);
        }

        _context.FAQs.Remove(faq);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
