using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;

namespace IMS.Post.IBLL.Services;

public interface IPostCommentService
{
    Task<PostCommentDto> CreatePostCommentDtoAsync(string userId, CreatePostCommentDto createPostCommentDto);

    Task<PostCommentDto> EditPostCommentDtoAsync(string userId, string commentId,
        UpdatePostCommentDto updatePostCommentDto);
    
    Task<IEnumerable<PostCommentDto>> GetPostCommentsAsync(
        string postId);
    
    Task DeletePostCommentAsync(string userId, string commentId);
}