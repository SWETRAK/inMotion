using AutoMapper;
using IMS.Post.Models.Dto.Outgoing;

namespace IMS.Post.Models.Mappers;

public class PostProfile: Profile
{
    public PostProfile()
    {
        CreateMap<Domain.Entities.Post.Post, GetPostResponseDto>()
            .ForMember(x => x.Author, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(x => x.Description, opt => opt.MapFrom(p => p.Description))
            .ForMember(x => x.Title, opt => opt.MapFrom(p => p.Title))
            .ForMember(x => x.Tags, opt => opt.MapFrom(p => p.Tags))
            .ForMember(x => x.Localization, opt => opt.MapFrom(p => p.Localization))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(p => new[] { p.FrontVideo, p.RearVideo }))
            .ForMember(x => x.PostCommentsCount, opt => opt.MapFrom(p => p.PostComments.Count()))
            .ForMember(x => x.PostReactionsCount, opt => opt.MapFrom(p => p.PostReactions.Count()))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(p => p.CreationDate));
    }
}