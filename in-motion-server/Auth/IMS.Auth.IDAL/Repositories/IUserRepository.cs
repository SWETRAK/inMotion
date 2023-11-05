using IMS.Auth.Domain.Entities;

namespace IMS.Auth.IDAL.Repositories;

public interface IUserRepository: IDisposable
{
    User GetByEmail(string email);
    Task<User> GetByEmailAsync(string email);
    // Task<User> GetByIdAsync(Guid userId);
    Task<IList<User>> GetManyByIdRangeAsync(IEnumerable<Guid> userIds);
    Task<User> GetByIdWithProvidersAsync(Guid userId);
    Task Insert(User user);
    Task Save(); 
}