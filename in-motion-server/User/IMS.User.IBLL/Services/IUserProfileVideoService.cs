using IMS.User.Models.Dto.Outgoing;

namespace IMS.User.IBLL.Services;

public interface IUserProfileVideoService
{
    Task<UserProfileVideoDto> GetUserProfileVideoByAuthorAsync(string userId);
    Task<UserProfileVideoDto> GetUserProfileVideoByVideoIdAsync(string videoId);
}