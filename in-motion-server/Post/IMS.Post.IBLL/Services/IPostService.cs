using IMS.Post.Models.Dto.Incoming;
using IMS.Post.Models.Dto.Outgoing;
using IMS.Shared.Models.Dto;

namespace IMS.Post.IBLL.Services;

public interface IPostService
{
    // TODO: Get public posts by day (paginated)
    Task<ImsPagination<IEnumerable<GetPostResponseDto>>> GetPublicPostsFromCurrentDay(
        ImsPaginationRequestDto paginationRequestDto);
    
    // TODO: Get posts from your friends (paginated)
    Task<ImsPagination<IEnumerable<GetPostResponseDto>>> GetFriendsPublicPostsFromCurrentDay(
        ImsPaginationRequestDto paginationRequestDto);

    // TODO: Create post
    Task<CreatePostResponseDto> CreatePost(string userId, CreatePostRequestDto createPostRequestDto);
    
    // TODO: Get your current post
    Task<GetPostResponseDto> GetCurrentUserPost(string userId);

    // TODO: Edit your post meats
}