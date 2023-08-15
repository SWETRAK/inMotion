using IMS.Friends.Domain.Entities;

namespace IMS.Friends.IDAL.Repositories;

public interface IFriendshipRepository: IDisposable
{
    Task<Friendship> GetById(Guid id);
    Task<Friendship> GetByUsersId(Guid firstUserId, Guid secondUserId);
    Task<IEnumerable<Friendship>> GetRequested(Guid id);
    Task<IEnumerable<Friendship>> GetAccepted(Guid id);
    Task<IEnumerable<Friendship>> GetRejected(Guid id);
    Task InsertAsync(Friendship friendship);
    Task SaveAsync();
}