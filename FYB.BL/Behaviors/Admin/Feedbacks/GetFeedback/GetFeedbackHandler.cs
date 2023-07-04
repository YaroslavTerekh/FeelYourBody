using AutoMapper;
using FYB.BL.Exceptions;
using FYB.Data.Common.DataTransferObjects;
using FYB.Data.Constants;
using FYB.Data.DbConnection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Behaviors.Admin.Feedbacks.GetFeedback;

public class GetFeedbackHandler : IRequestHandler<GetFeedbackCommand, FeedbackDTO>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetFeedbackHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FeedbackDTO> Handle(GetFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await _context.Feedbacks
            .Include(t => t.Photos)
            .Select(t => _mapper.Map<FeedbackDTO>(t))
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (feedback is null) throw new NotFoundException(ErrorMessages.FeedbackNotFound);

        return feedback;
    }
}
