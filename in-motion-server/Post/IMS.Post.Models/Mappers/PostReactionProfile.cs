using AutoMapper;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.Models.Dto.Outgoing;  

namespace IMS.Post.Models.Mappers;

public class PostReactionProfile: Profile
{
    public PostReactionProfile()
    {
        CreateMap<PostReaction, PostReactionDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(x => x.PostId, opt => opt.MapFrom(p => p.PostId.ToString()))
            .ForMember(x => x.Author, opt => opt.Ignore())
            .ForMember(x => x.Emoji, opt => opt.MapFrom(p => p.Emoji))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(p => p.CreationDate));
    }
}