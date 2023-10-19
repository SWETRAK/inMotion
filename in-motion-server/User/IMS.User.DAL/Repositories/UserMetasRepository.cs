using IMS.User.Domain;
using IMS.User.Domain.Entities;
using IMS.User.IDAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMS.User.DAL.Repositories;

public class UserMetasRepository: IUserMetasRepository
{
    private readonly ImsUserDbContext _context;
    private bool _disposed = false;

    public UserMetasRepository(ImsUserDbContext context)
    {
        _context = context;
    }

    public async Task<UserMetas> GetByIdAsync(Guid id)
    {
        return await _context.UserMetas.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<UserMetas> GetByIdWithProfileVideoAsync(Guid id)
    {
        return await _context.UserMetas
            .Include(u => u.ProfileVideo)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<UserMetas> GetByExternalUserIdAsync(Guid externalUserId)
    {
        return await _context.UserMetas.FirstOrDefaultAsync(x => x.UserExternalId.Equals(externalUserId));
    }

    public async Task<UserMetas> GetByExternalUserIdWithProfileVideoAsync(Guid externalUserId)
    {
        return await _context.UserMetas
            .Include(u => u.ProfileVideo)
            .FirstOrDefaultAsync(x => x.UserExternalId.Equals(externalUserId));
    }

    public async Task<IList<UserMetas>> GetByExternalUsersIdWithProfileVideoAsync(IEnumerable<Guid> externalUserId)
    {
        return await _context.UserMetas
            .Include(x => x.ProfileVideo)
            .Where(x => externalUserId.Contains(x.UserExternalId))
            .ToListAsync();
    }

    public async Task<UserMetas> GetByProfileVideoIdAsync(Guid profileVideoId)
    {
        return await _context.UserMetas.FirstOrDefaultAsync(x => x.ProfileVideoId.Equals(profileVideoId));
    }

    public async Task<UserMetas> GetByProfileVideoIdWithProfileVideoAsync(Guid profileVideoId)
    {
        return await _context.UserMetas
            .Include(x => x.ProfileVideo)
            .FirstOrDefaultAsync(x => x.ProfileVideoId.Equals(profileVideoId));
    }

    public void RemoveAsync(UserMetas userMetas)
    {
        _context.Remove(userMetas);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
