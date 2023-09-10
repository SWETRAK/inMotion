using IMS.User.Domain.Entities;

namespace IMS.User.IDAL.Repositories;

public interface IUserMetaDataRepository: IDisposable
{
    Task<UserMetaData> GetByIdAsync(Guid id);
    Task<UserMetaData> GetByIdWithProfileVideoAsync(Guid id);
    Task<UserMetaData> GetByExternalUserIdAsync(Guid externalUserId);
    Task<UserMetaData> GetByExternalUserIdWithProfileVideoAsync(Guid externalUserId);
    Task<IList<UserMetaData>> GetByExternalUsersIdWithProfileVideoAsync(IEnumerable<Guid>externalUserId);
    Task<UserMetaData> GetByProfileVideoIdAsync(Guid profileVideoId);
    Task<UserMetaData> GetByProfileVideoIdWithProfileVideoAsync (Guid profileVideoId);
    void RemoveAsync(UserMetaData userMetaData);
    Task SaveAsync();
}
