using IMS.Post.Domain;
using IMS.Post.IDAL.Repositories.Post;

namespace IMS.Post.DAL.Repositories.Post;

public class PostVideoRepository: IPostVideoRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public PostVideoRepository(ImsPostDbContext context)
    {
        _context = context;
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