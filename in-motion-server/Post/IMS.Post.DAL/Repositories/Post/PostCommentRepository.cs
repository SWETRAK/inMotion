using IMS.Post.Domain;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IDAL.Repositories.Post;
using Microsoft.EntityFrameworkCore;

namespace IMS.Post.DAL.Repositories.Post;

public class PostCommentRepository: IPostCommentRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public PostCommentRepository(ImsPostDbContext context)
    {
        _context = context;
    }

    public async Task<PostComment> GetByIdAndAuthorIdAndPostIdAsync(Guid id, Guid authorId, Guid postId)
    {
        return await _context.PostComments.FirstOrDefaultAsync(x =>
            x.Id.Equals(id) && x.ExternalAuthorId.Equals(authorId) && x.PostId.Equals(postId));
    }

    public async Task<IList<PostComment>> GetRangeByPostIdPaginatedAsync(Guid postId, int pageNumber, int pageSize)
    {
        return await _context.PostComments.Take(pageSize).Skip((pageNumber - 1) * pageSize)
            .Where(x => x.PostId.Equals(postId)).ToListAsync();
    }

    public async Task<long> GetRangeByPostIdCountAsync(Guid postId)
    {
        return await _context.PostComments.CountAsync(x => x.PostId.Equals(postId));
    }

    public async Task<PostComment> GetByIdAndAuthorIdAsync(Guid id, Guid authorId)
    {
        return await _context.PostComments.FirstOrDefaultAsync(x =>
            x.Id.Equals(id) && x.ExternalAuthorId.Equals(authorId));
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Delete(PostComment postComment)
    {
        _context.PostComments.Remove(postComment);
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