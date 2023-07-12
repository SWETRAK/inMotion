using IMS.Shared.Domain.Entities.User;

namespace IMS.Auth.IDAL.Repositories;

public interface IUserRepository: IDisposable
{
    User GetByEmail(string email);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(Guid userId);
    Task Insert(User user);
    Task Save(); 
}