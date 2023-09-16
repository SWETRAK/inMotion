using AutoMapper;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.Models.Dto.Outgoing;

namespace IMS.Post.Models.Mappers;

public class PostCommentReactionProfile: Profile
{
    public PostCommentReactionProfile()
    {
        CreateMap<PostCommentReaction, PostCommentReactionDto>()
            .ForMember(x => x.Author, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.MapFrom(p => p.Id.ToString()))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(p => p.CreationDate))
            .ForMember(x => x.Emoji, opt => opt.MapFrom(p => p.Emoji))
            .ForMember(x => x.PostCommentId, opt => opt.MapFrom(p => p.PostCommentId.ToString()));

    }
}