using IMS.Auth.Domain;
using IMS.Auth.Domain.Entities;
using IMS.Auth.IDAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMS.Auth.DAL.Repositories;

public sealed class UserRepository: IUserRepository
{
    private readonly ImsAuthDbContext _context;
    private bool _disposed = false;

    public UserRepository(ImsAuthDbContext context)
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

    public async Task<User> GetByIdWithProvidersAsync(Guid userId)
    {
        return await _context
            .Users
            .Include(x => x.Providers)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task Insert(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
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