using AutoMapper;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.Models.Dto.Outgoing;

namespace IMS.Post.Models.Mappers;

public class PostCommentProfile: Profile
{
    public PostCommentProfile()
    {
        CreateMap<PostComment, PostCommentDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.ToString()))
            .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Content))
            .ForMember(d => d.PostId, opt => opt.MapFrom(s => s.PostId))
            .ForMember(d => d.Author, opt => opt.Ignore())
            .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreationDate));
    }
}