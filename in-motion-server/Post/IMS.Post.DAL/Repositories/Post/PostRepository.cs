using IMS.Post.Domain;
using IMS.Post.Domain.Consts;
using IMS.Post.IDAL.Repositories.Post;
using Microsoft.EntityFrameworkCore;

using PostEntity = IMS.Post.Domain.Entities.Post.Post;

namespace IMS.Post.DAL.Repositories.Post;

public class PostRepository: IPostRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public PostRepository(ImsPostDbContext context)
    {
        _context = context;
    }
    
    public async Task<IList<PostEntity>> GetPublicFormIterationPaginatedAsync(Guid postIterationId, int pageNumber, int pageSize = 20)
    {
        return await _context.Posts
            .Take(pageSize)
            .Skip((pageNumber - 1) * pageSize)
            .Include(x => x.Videos)
            .Include(x => x.Tags)
            .Include(x => x.PostComments)
            .Include(x => x.PostReactions)
            .Where(x => x.IterationId.Equals(postIterationId) && x.Visibility.Equals(PostVisibility.Public))
            .ToArrayAsync();
    }

    public async Task<PostEntity> GetByExternalAuthorIdAsync(Guid postIterationId, Guid externalAuthorId)
    {
        return await _context.Posts
            .Include(x => x.Videos)
            .Include(x => x.Tags)
            .Include(x => x.PostComments)
            .Include(x => x.PostReactions)
            .FirstOrDefaultAsync(x =>
                x.ExternalAuthorId.Equals(externalAuthorId) && x.IterationId.Equals(postIterationId));
    }

    public async Task<PostEntity> GetByIdAndAuthorIdAsync(Guid postIterationId, Guid postId, Guid userId)
    {
        return await _context.Posts
            .Include(x => x.Videos)
            .Include(x => x.Tags)
            .Include(x => x.PostComments)
            .Include(x => x.PostReactions)
            .FirstOrDefaultAsync(x =>
                x.Id.Equals(postId) && x.ExternalAuthorId.Equals(userId) && x.IterationId.Equals(postIterationId));
    }

    public async Task<PostEntity> GetByIdAndAuthorIdAsync(Guid postId, Guid userId)
    {
        return await _context.Posts
            .Include(x => x.Videos)
            .Include(x => x.Tags)
            .Include(x => x.PostComments)
            .Include(x => x.PostReactions)
            .FirstOrDefaultAsync(x =>
                x.Id.Equals(postId) && x.ExternalAuthorId.Equals(userId));
    }

    public async Task<PostEntity> GetByIdAsync(Guid postId)
    {
        return await _context.Posts
            .Include(x => x.Videos)
            .Include(x => x.Tags)
            .Include(x => x.PostComments)
            .Include(x => x.PostReactions)
            .FirstOrDefaultAsync(x => x.Id.Equals(postId));
    }

    public async Task AddAsync(PostEntity post)
    {
        await _context.Posts.AddAsync(post);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IList<PostEntity>> GetFriendsPublicAsync(Guid postIterationId, IEnumerable<Guid> friendIds)
    {
        return await _context.Posts
            .Include(x => x.Videos)
            .Include(x => x.Tags)
            .Include(x => x.PostComments)
            .Include(x => x.PostReactions)
            .Where(x => x.IterationId.Equals(postIterationId) && x.Visibility.Equals(PostVisibility.Public) &&
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