using IMS.Shared.Domain.Entities.User;

namespace IMS.Auth.IDAL.Repositories;

public interface IUserRepository: IDisposable
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(Guid userId);
    void Insert(User user);
    void Save(); 
}