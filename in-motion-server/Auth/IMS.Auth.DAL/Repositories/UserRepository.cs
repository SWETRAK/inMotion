using IMS.Auth.IDAL.Repositories;
using IMS.Shared.Domain;
using IMS.Shared.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace IMS.Auth.DAL.Repositories;

public class UserRepository: IUserRepository
{
    private readonly DomainDbContext _context;
    private bool _disposed = false;

    public UserRepository(DomainDbContext context)
    {
        _context = context;
    }
    
    public User GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }
    
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetByIdAsync(Guid userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task Insert(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this._disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}