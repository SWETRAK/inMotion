using AutoMapper;
using IMS.Post.Domain.Entities.Other;
using IMS.Post.Models.Dto.Outgoing;

namespace IMS.Post.Models.Mappers;

public class TagProfile: Profile
{
    public TagProfile()
    {
        CreateMap<Tag, PostTagDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dto => dto.CreateAt, opt => opt.MapFrom(src => src.CreationDate));
        
    }
}