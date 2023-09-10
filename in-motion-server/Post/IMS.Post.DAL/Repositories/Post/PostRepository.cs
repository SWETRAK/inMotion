using IMS.Post.Domain;
using IMS.Post.Domain.Consts;
using IMS.Post.IDAL.Repositories.Post;
using Microsoft.EntityFrameworkCore;

namespace IMS.Post.DAL.Repositories.Post;

public class PostRepository: IPostRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public PostRepository(ImsPostDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Post.Post>> GetPublicFormDayPaginatedAsync(DateTime dateTime, int pageNumber, int pageSize = 20)
    {
        return await _context.Posts.Take(pageSize).Skip((pageNumber - 1) * pageSize)
            .Where(x => x.CreationDate.Date.Equals(dateTime.Date) && x.Visibility.Equals(PostVisibility.Public)).ToArrayAsync();
    }

    public async Task<Domain.Entities.Post.Post> GetByExternalAuthorIdAsync(DateTime dateTime, Guid externalAuthorId)
    {
        return await _context.Posts.FirstOrDefaultAsync(x => x.ExternalAuthorId.Equals(externalAuthorId) && x.CreationDate.Date.Equals(dateTime.Date));
    }

    public async Task<Domain.Entities.Post.Post> GetByIdAndAuthorIdAsync(DateTime dateTime, Guid postId, Guid userId)
    {
        return await _context.Posts.FirstOrDefaultAsync(x =>
            x.Id.Equals(postId) && x.ExternalAuthorId.Equals(userId) && x.CreationDate.Date.Equals(dateTime.Date));
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Post.Post>> GetFriendsPublicFromDayPaginatedAsync(DateTime dateTime,
        IEnumerable<Guid> friendIds, int pageNumber, int pageSize = 20)
    {
        return await _context.Posts.Take(pageSize).Skip((pageNumber - 1) * pageSize)
            .Where(x => x.CreationDate.Date.Equals(dateTime.Date) && x.Visibility.Equals(PostVisibility.Public) &&
                        friendIds.Contains(x.ExternalAuthorId))
            .ToArrayAsync();
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