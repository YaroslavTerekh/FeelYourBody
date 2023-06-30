using FYB.Data.DbConnection;
using FYB.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.FrequentlyAskedQuestions.GetAllFAQ;

public class GetAllFAQHandler : IRequestHandler<GetAllFAQQuery, List<FAQ>>
{
    private readonly DataContext _context;

    public GetAllFAQHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<FAQ>> Handle(GetAllFAQQuery request, CancellationToken cancellationToken)
    {
        return await _context.FAQs.ToListAsync(cancellationToken);
    }
}
