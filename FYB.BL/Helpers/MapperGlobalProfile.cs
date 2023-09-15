using AutoMapper;
using FYB.BL.Services.Abstractions;
using FYB.BL.Services.Realizations;
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
        CreateMap<Coaching, CoachingDTO>()
            .ForMember(dest => dest.AccessDays, opt => opt.MapFrom(src => src.UnixExpireTime));
        CreateMap<Feedback, FeedbackDTO> ();
        CreateMap<Food, FoodDTO>()
            .ForMember(dest => dest.AccessDays, opt => opt.MapFrom(src => src.UnixExpireTime));
        CreateMap<FoodPoint, FoodPointDTO> ();
        CreateMap<CoachingDetails, CoachingDetailDTO>();
        CreateMap<CoachingDetailsParent, CoachingDetailsParentDTO>();
        CreateMap<FoodPointParent, FoodPointsParentDTO>();
    }
}
