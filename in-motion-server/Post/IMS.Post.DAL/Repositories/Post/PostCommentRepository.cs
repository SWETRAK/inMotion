using IMS.Post.Domain;
using IMS.Post.IDAL.Repositories.Post;

namespace IMS.Post.DAL.Repositories.Post;

public class PostCommentRepository: IPostCommentRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public PostCommentRepository(ImsPostDbContext context)
    {
        _context = context;
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