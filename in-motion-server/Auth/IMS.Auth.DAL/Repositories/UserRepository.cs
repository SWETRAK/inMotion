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
        return await _context.Users
            .Include(u => u.Providers)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IList<User>> GetManyByNickname(string nickname)
    {
        return await _context.Users
            .Where(u => EF.Functions.ILike(u.Nickname, $"%{nickname}%") && u.ConfirmedAccount.Equals(true))
            .Take(30)
            .ToListAsync();
    }
    
    public async Task<IList<User>> GetManyByIdRangeAsync(IEnumerable<Guid> userIds)
    {
        return await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
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