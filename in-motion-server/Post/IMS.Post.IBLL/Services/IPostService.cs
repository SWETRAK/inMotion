using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;

namespace IMS.Post.IBLL.Services;

public interface IPostService
{
    Task<ImsPagination<IList<GetPostResponseDto>>> GetPublicPostsFromCurrentIteration(
        ImsPaginationRequestDto paginationRequestDto);
    
    Task<IList<GetPostResponseDto>> GetFriendsPublicPostsFromCurrentIteration(
        string userId);
    
    Task<CreatePostResponseDto> CreatePost(string userId, CreatePostRequestDto createPostRequestDto);
    
    Task<GetPostResponseDto> GetCurrentUserPost(string userId);
    
    Task<GetPostResponseDto> EditPostsMetas(string userId, string postId,
        EditPostRequestDto editPostRequestDto);
}