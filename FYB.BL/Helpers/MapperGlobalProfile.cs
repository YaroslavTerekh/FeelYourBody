using AutoMapper;
using FYB.BL.Services.Abstractions;
using FYB.BL.Services.Realizations;
using FYB.BL.Settings.Realizations;
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
    public MapperGlobalProfile(HostSettings hostSettings)
    {
        CreateMap<AppFile, AppFileDTO>()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => String.Concat(hostSettings.ApplicationUrl, src.FilePath.Replace(@"\", "/"))));
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
        CreateMap<CoachingVideo, CoachingVideoDTO>()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.Path.Replace(@"\", "/")));
        CreateMap<FoodDetail, FoodDetailDTO>();
        CreateMap<FoodAvatar, BaseFileDTO> ()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => String.Concat(hostSettings.ApplicationUrl, src.FilePath.Replace(@"\", "/"))));
        CreateMap<FoodPhoto, BaseFileDTO> ()
            .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => String.Concat(hostSettings.ApplicationUrl, src.FilePath.Replace(@"\", "/"))));
    }
}
