using IMS.User.Models.Dto.Outgoing;

namespace IMS.User.IBLL.Services;

public interface IUserService
{
    Task<FullUserInfoDto> GetFullUserInfoAsync(string userIdString);
    Task<IEnumerable<FullUserInfoDto>> GetFullUsersInfoAsync(IList<string> userIds);
}