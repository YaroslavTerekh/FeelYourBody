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

namespace FYB.BL.Behaviors.Foods.GetAllFood;

public class GetAllFoodHandler : IRequestHandler<GetAllFoodQuery, List<FoodDTO>>
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GetAllFoodHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FoodDTO>> Handle(GetAllFoodQuery request, CancellationToken cancellationToken)
    {
        return await _context.Food
            .Include(t => t.FoodPoints)
            .Select(t => _mapper.Map<FoodDTO>(t))
            .ToListAsync(cancellationToken);
    }
}
