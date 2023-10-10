using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;

namespace IMS.Post.IBLL.Services;

public interface IPostCommentService
{
    Task<PostCommentDto> CreatePostCommentDtoAsync(string userId, CreatePostCommentDto createPostCommentDto);

    Task<PostCommentDto> EditPostCommentDtoAsync(string userId, string commentId,
        UpdatePostCommentDto updatePostCommentDto);
    
    Task<ImsPagination<IEnumerable<PostCommentDto>>> GetPostCommentsPaginatedAsync(
        string postId, ImsPaginationRequestDto imsPaginationRequestDto);
    
    Task DeletePostCommentAsync(string userId, string commentId);
}