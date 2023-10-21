using IMS.User.Domain.Entities;

namespace IMS.User.IDAL.Repositories;

public interface IUserMetasRepository: IDisposable
{
    Task<UserMetas> GetByIdAsync(Guid id);
    Task<UserMetas> GetByIdWithProfileVideoAsync(Guid id);
    Task<UserMetas> GetByExternalUserIdAsync(Guid externalUserId);
    Task<UserMetas> GetByExternalUserIdWithProfileVideoAsync(Guid externalUserId);
    Task<IList<UserMetas>> GetByExternalUsersIdWithProfileVideoAsync(IEnumerable<Guid>externalUserId);
    Task<UserMetas> GetByProfileVideoIdAsync(Guid profileVideoId);
    Task<UserMetas> GetByProfileVideoIdWithProfileVideoAsync (Guid profileVideoId);
    void RemoveAsync(UserMetas userMetas);
    Task Add(UserMetas userMetas);
    Task SaveAsync();
}
