using IMS.User.Domain.Entities;

namespace IMS.User.IDAL.Repositories;

public interface IUserProfileVideoRepository: IDisposable
{
    Task<UserProfileVideo> GetByIdAsync(Guid id);
    Task<UserProfileVideo> GetByIdWithUserMetasAsync(Guid id);
    Task<UserProfileVideo> GetByAuthorIdAsync(Guid authorId);
    Task<UserProfileVideo> GetByAuthorIdWithUserMetasAsync(Guid authorId);
    Task<UserProfileVideo> GetByUserMetasIdAsync(Guid userMetasId);
    Task<UserProfileVideo> GetByUserMetasIdWithUserMetasAsync(Guid userMetasId);
    void RemoveAsync(UserProfileVideo userProfileVideo);
    Task SaveAsync();
}
