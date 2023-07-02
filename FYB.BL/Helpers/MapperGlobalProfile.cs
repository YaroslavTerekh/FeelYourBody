using AutoMapper;
using FYB.Data.Common.DataTransferObjects;
using FYB.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Helpers;

public class MapperGlobalProfile : Profile
{
    public MapperGlobalProfile()
    {
        CreateMap<AppFile, AppFileDTO>();
        CreateMap<Coach, CoachDTO> ();
        CreateMap<Coaching, CoachingDTO> ();
        CreateMap<Feedback, FeedbackDTO> ();
        CreateMap<Food, FoodDTO> ();
        CreateMap<FoodPoint, FoodPointDTO> ();
    }
}
