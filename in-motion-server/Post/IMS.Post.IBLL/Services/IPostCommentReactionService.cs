using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;

namespace IMS.Post.IBLL.Services;

public interface IPostCommentReactionService
{

    Task<PostCommentReactionDto> AddPostCommentReaction(string userId,
        CreatePostCommentReactionDto createPostCommentReactionDto);

    Task<PostCommentReactionDto> EditPostCommentReaction(string userId,
        string postCommentReactionId,
        EditPostCommentReactionDto editPostCommentReactionDto);

    Task RemovePostCommentReaction(string userId, string postCommentReactionId);

    Task<IEnumerable<PostCommentReactionDto>> GetAllPostCommentReactionsPaginatedAsync(
        string postCommentId);
}