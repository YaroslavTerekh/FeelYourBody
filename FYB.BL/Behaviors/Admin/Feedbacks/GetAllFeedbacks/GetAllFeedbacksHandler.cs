using AutoMapper;
using FYB.Data.Common.DataTransferObjects;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.GetAllFeedbacks;

public class GetAllFeedbacksHandler : IRequestHandler<GetAllFeedbacksQuery, List<FeedbackDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetAllFeedbacksHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FeedbackDTO>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = await _context.Feedbacks.Select(t => _mapper.Map<FeedbackDTO>(t)).ToListAsync(cancellationToken);

        return feedbacks;
    }
}
