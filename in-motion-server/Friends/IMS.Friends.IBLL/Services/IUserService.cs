using IMS.Friends.Models.Models;

namespace IMS.Friends.IBLL.Services;

public interface IUserService
{
    Task<IEnumerable<UserInfo>> GetUsersByIdsArray(IEnumerable<Guid> idArray);
    Task<UserInfo> GetUserByIdArray(Guid userId);
}