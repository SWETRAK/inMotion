using IMS.Post.Domain;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IDAL.Repositories.Post;
using Microsoft.EntityFrameworkCore;

namespace IMS.Post.DAL.Repositories.Post;

public class PostReactionRepository: IPostReactionRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public PostReactionRepository(ImsPostDbContext context)
    {
        _context = context;
    }

    public async Task<PostReaction> GetByAuthorIdAndPostIdAsync(Guid authorId, Guid postId)
    {
        return await _context.PostReactions.FirstOrDefaultAsync(x =>
            x.ExternalAuthorId == authorId && x.PostId == postId);
    }

    public async Task<PostReaction> GetByIdAndUserIdAndPostIdAsync(Guid id, Guid userId, Guid postId)
    {
        return await _context.PostReactions.FirstOrDefaultAsync(x =>
            x.Id == id && x.ExternalAuthorId == userId && x.PostId == postId);
    }

    public async Task<PostReaction> GetByIdAndAuthorId(Guid id, Guid authorId)
    {
        return await _context.PostReactions.FirstOrDefaultAsync(x =>
            x.Id.Equals(id) && x.ExternalAuthorId.Equals(authorId));
    }

    public async Task<IList<PostReaction>> GetRangeByPostIdAsync(Guid postId)
    {
        return await _context.PostReactions
            .Where(x => x.PostId.Equals(postId)).ToListAsync();
    }

    public async Task AddAsync(PostReaction postReaction)
    {
        await _context.PostReactions.AddAsync(postReaction);
    }

    public async Task<long> GetRangeByPostIdCountAsync(Guid postId)
    {
        return await _context.PostReactions.CountAsync(x => x.PostId.Equals(postId));
    }

    public async Task<PostReaction> GetByIdAsync(Guid id)
    {
        return await _context.PostReactions.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public void Delete(PostReaction postReaction)
    { 
        _context.PostReactions.Remove(postReaction);
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