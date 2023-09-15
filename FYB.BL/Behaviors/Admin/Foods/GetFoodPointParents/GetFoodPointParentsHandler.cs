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

namespace FYB.BL.Behaviors.Admin.Foods.GetFoodPointParents;

public class GetFoodPointParentsHandler : IRequestHandler<GetFoodPointParentsQuery, List<FoodPointsParentDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetFoodPointParentsHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FoodPointsParentDTO>> Handle(GetFoodPointParentsQuery request, CancellationToken cancellationToken)
    {
        return await _context.FoodPointParents
            .Where(t => t.FoodId == request.FoodID)
            .OrderBy(t => t.DayNumber)
            .Select(t => _mapper.Map<FoodPointsParentDTO>(t))
            .ToListAsync(cancellationToken);
    }
}
