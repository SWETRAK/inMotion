using IMS.User.Domain;
using IMS.User.Domain.Entities;
using IMS.User.IDAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMS.User.DAL.Repositories;

public class UserProfileVideoRepository: IUserProfileVideoRepository
{
    private readonly ImsUserDbContext _context;
    private bool _disposed;

    public UserProfileVideoRepository(ImsUserDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfileVideo> GetByIdAsync(Guid id)
    {
        return await _context.UserProfileVideos
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<UserProfileVideo> GetByIdWithUserMetasAsync(Guid id)
    {
        return await _context.UserProfileVideos
            .Include(u => u.UserMetas)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<UserProfileVideo> GetByAuthorIdAsync(Guid authorId)
    {
        return await _context.UserProfileVideos
            .FirstOrDefaultAsync(x => x.AuthorExternalId.Equals(authorId));
    }

    public async Task<UserProfileVideo> GetByAuthorIdWithUserMetasAsync(Guid authorId)
    {
        return await _context.UserProfileVideos
            .Include(x => x.UserMetas)
            .FirstOrDefaultAsync(x => x.AuthorExternalId.Equals(authorId));
    }

    public async Task<UserProfileVideo> GetByUserMetasIdAsync(Guid userMetasId)
    {
        return await _context.UserProfileVideos
            .FirstOrDefaultAsync(x => x.UserMetasId.Equals(userMetasId));
    }

    public async Task<UserProfileVideo> GetByUserMetasIdWithUserMetasAsync(Guid userMetasId)
    {
        return await _context.UserProfileVideos
            .Include(x => x.UserMetas)
            .FirstOrDefaultAsync(x => x.UserMetasId.Equals(userMetasId));
    }

    public void RemoveAsync(UserProfileVideo userProfileVideo)
    {
        _context.UserProfileVideos.Remove(userProfileVideo);
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
