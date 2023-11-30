using AutoMapper;
using IMS.Post.Models.Dto.Outgoing;

using PostEntity = IMS.Post.Domain.Entities.Post.Post;

namespace IMS.Post.Models.Mappers;

public class PostProfile: Profile
{
    public PostProfile()
    {
        CreateMap<PostEntity, GetPostResponseDto>()
            .ForMember(x => x.Author, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(x => x.Description, opt => opt.MapFrom(p => p.Description))
            .ForMember(x => x.Title, opt => opt.MapFrom(p => p.Title))
            .ForMember(x => x.Tags, opt => opt.MapFrom(p => p.Tags))
            .ForMember(x => x.Videos, opt => opt.MapFrom(p => p.Videos))
            .ForMember(x => x.PostCommentsCount, opt => opt.MapFrom(p => p.PostComments.Count()))
            .ForMember(x => x.PostReactionsCount, opt => opt.MapFrom(p => p.PostReactions.Count()))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(p => p.CreationDate));

        CreateMap<PostEntity, CreatePostResponseDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(x => x.Description, opt => opt.MapFrom(p => p.Description))
            .ForMember(x => x.Title, opt => opt.MapFrom(p => p.Title))
            .ForMember(x => x.Tags, opt => opt.MapFrom(p => p.Tags));
    }
}