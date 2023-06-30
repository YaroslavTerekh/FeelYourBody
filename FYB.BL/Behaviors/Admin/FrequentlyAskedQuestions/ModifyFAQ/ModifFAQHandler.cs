using FYB.BL.Exceptions;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FYB.BL.Behaviors.Admin.FrequentlyAskedQuestions.ModifyFAQ;

public class ModifFAQHandler : IRequestHandler<ModifyFAQCommand>
{
    private readonly DataContext _context;

    public ModifFAQHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(ModifyFAQCommand request, CancellationToken cancellationToken)
    {
        var faq = await _context.FAQs.FirstOrDefaultAsync(t => t.Id == request.FAQId, cancellationToken);

        if(faq is null)
        {
            throw new NotFoundException(ErrorMessages.FAQNotFound);
        }

        faq.Question = request.Question;
        faq.Answer = request.Answer;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
