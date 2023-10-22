using IMS.User.Domain.Entities;

namespace IMS.User.IDAL.Repositories;

public interface IUserProfileVideoRepository: IDisposable
{
    Task<UserProfileVideo> GetByIdAsync(Guid id);
    Task<UserProfileVideo> GetByIdWithUserMetasAsync(Guid id);
    Task<UserProfileVideo> GetByAuthorIdAsync(Guid authorId);
    Task<UserProfileVideo> GetByAuthorIdWithUserMetasAsync(Guid authorId);
    void RemoveAsync(UserProfileVideo userProfileVideo);
    Task SaveAsync();
}
