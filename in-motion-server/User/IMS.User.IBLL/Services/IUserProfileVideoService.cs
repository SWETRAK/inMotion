using IMS.User.Models.Dto.Incoming;

namespace IMS.User.IBLL.Services;

public interface IUserProfileVideoService
{
    Task UpdateUserProfileVideo(string userIdString, UpdateUserProfileVideoDto updateUserProfileVideoDto);
}