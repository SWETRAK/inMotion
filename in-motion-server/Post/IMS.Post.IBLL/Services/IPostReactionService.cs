using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;

namespace IMS.Post.IBLL.Services;

public interface IPostReactionService
{
    Task<IEnumerable<PostReactionDto>> GetForPostAsync(string postId);
    
    Task<PostReactionDto> EditPostReactionAsync(string userId, string reactionId,
        EditPostReactionDto editPostRequestDto);
    
    Task DeletePostReactionAsync(string userId, string reactionId);
    
    Task<PostReactionDto> CreatePostReactionAsync(string userId, CreatePostReactionDto createPostReactionDto);
}