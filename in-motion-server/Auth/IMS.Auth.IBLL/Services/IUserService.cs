using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;

namespace IMS.Auth.IBLL.Services;

public interface IUserService
{
    Task<UserInfoDto> UpdateUserEmail(UpdateEmailDto updateEmailDto, string userIdString);

    Task<UserInfoDto> UpdateUserNickname(UpdateNicknameDto updateNicknameDto, string userIdString);

    Task<UserInfoDto> GetUserInfo(string userIdString);

    Task<IEnumerable<UserInfoDto>> GetUsersByNickname(string nickname);

    Task<IEnumerable<UserInfoDto>> GetUsersInfo(IEnumerable<string> userIdStrings);
}