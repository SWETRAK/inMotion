using IMS.Post.Models.Models.Author;

namespace IMS.Post.IBLL.Services;

public interface IUserService
{
    Task<IEnumerable<AuthorInfo>> GetUsersByIdsArray(IEnumerable<Guid> idArray);
    Task<AuthorInfo> GetUserById(Guid userId);
}