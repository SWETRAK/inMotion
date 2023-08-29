using IMS.Friends.Domain.Entities;

namespace IMS.Friends.IDAL.Repositories;

public interface IFriendshipRepository: IDisposable
{
    Task<Friendship> GetById(Guid id);
    Task<Friendship> GetByUsersId(Guid firstUserId, Guid secondUserId);
    Task<List<Friendship>> GetRequested(Guid id);
    Task<List<Friendship>> GetAccepted(Guid id);
    Task<List<Friendship>> GetInvitation(Guid id);
    Task<List<Friendship>> GetRejected(Guid id);
    void RemoveAsync(Friendship friendship);
    Task InsertAsync(Friendship friendship);
    Task SaveAsync();
}