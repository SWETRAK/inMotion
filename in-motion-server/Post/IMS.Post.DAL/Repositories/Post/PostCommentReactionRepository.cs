using IMS.Post.Domain;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IDAL.Repositories.Post;
using Microsoft.EntityFrameworkCore;

namespace IMS.Post.DAL.Repositories.Post;

public class PostCommentReactionRepository: IPostCommentReactionRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public PostCommentReactionRepository(ImsPostDbContext context)
    {
        _context = context;
    }

    public async Task<IList<PostCommentReaction>> GetByPostCommentIdPaginatedAsync(Guid postCommentId)
    {
        return await _context.PostCommentReactions
            .Where(x => x.PostCommentId.Equals(postCommentId)).ToListAsync();
    }

    public async Task<long> GetByPostCommentIdCountAsync(Guid postCommentId)
    {
        return await _context.PostCommentReactions
            .Where(x => x.PostCommentId.Equals(postCommentId)).CountAsync();
    }

    public async Task<PostCommentReaction> GetByIdAndAuthorIdAsync(Guid id, Guid authorId)
    {
        return await _context.PostCommentReactions.FirstOrDefaultAsync(x =>
            x.Id.Equals(id) && x.ExternalAuthorId.Equals(authorId));
    }

    public async Task<PostCommentReaction> GetByIdAndAuthorIdAndPostCommentIdAsync(Guid id, Guid authorId, Guid postCommentId)
    {
        return await _context.PostCommentReactions.FirstOrDefaultAsync(x =>
            x.Id.Equals(id) && x.PostCommentId.Equals(postCommentId) && x.ExternalAuthorId.Equals(authorId));
    }

    public async Task<PostCommentReaction> GetByAuthorIdAndPostCommentIdAsync(Guid authorId, Guid postCommentId)
    {
        return await _context.PostCommentReactions
            .FirstOrDefaultAsync(x => x.ExternalAuthorId.Equals(authorId) && 
                                      x.PostCommentId.Equals(postCommentId));
    }

    public void Remove(PostCommentReaction postCommentReaction)
    {
        _context.PostCommentReactions.Remove(postCommentReaction);
    }

    public async Task AddAsync(PostCommentReaction postCommentReaction)
    {
        await _context.PostCommentReactions.AddAsync(postCommentReaction);
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