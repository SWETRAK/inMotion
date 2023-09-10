using IMS.User.Models.Dto.Incoming;
using IMS.User.Models.Dto.Outgoing;

namespace IMS.User.IBLL.Services;

public interface IUserService
{
    Task<FullUserInfoDto> GetFullUserInfoAsync(string userIdString);
    Task<IEnumerable<FullUserInfoDto>> GetFullUsersInfoAsync(IList<string> userIds);

    Task<UpdatedUserBioDto> UpdateBioAsync(string userId, UpdateUserBioDto updateUserBioDto);

    Task<UpdatedUserProfileVideoDto> UpdateUserProfileVideo(string userId, UpdateUserProfileVideoDto updateUserProfileVideoDto);
}