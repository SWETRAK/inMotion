using IMS.Friends.Models.Models;

namespace IMS.Friends.IBLL.Services;

public interface IUserService
{
    Task<IEnumerable<UserInfo>> GetUsersFromIdArray(IEnumerable<Guid> idArray);
}