using AutoMapper;
using IMS.Post.Domain.Entities.Other;
using IMS.Post.Models.Dto.Outgoing;

namespace IMS.Post.Models.Mappers;

public class LocalizationProfile: Profile
{
    public LocalizationProfile()
    {
        CreateMap<Localization, PostLocalizationDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dto => dto.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dto => dto.Longitude, opt => opt.MapFrom(src => src.Longitude));
    }
}