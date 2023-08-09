using IMS.Auth.Models.Dto.Incoming;
using IMS.Auth.Models.Dto.Outgoing;

namespace IMS.Auth.IBLL.Services;

public interface IUserService
{
    public Task<UserInfoDto> UpdateUserEmail(UpdateEmailDto updateEmailDto, string userIdString);

    public Task<UserInfoDto> UpdateUserNickname(UpdateNicknameDto updateNicknameDto, string userIdString);
}