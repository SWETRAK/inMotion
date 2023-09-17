using IMS.Post.Domain;
using IMS.Post.Domain.Entities.Post;
using IMS.Post.IDAL.Repositories.Post;
using Microsoft.EntityFrameworkCore;

namespace IMS.Post.DAL.Repositories.Post;

public class PostIterationRepository: IPostIterationRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public PostIterationRepository(ImsPostDbContext context)
    {
        _context = context;
    }
    
    public async Task<PostIteration> GetById(Guid id)
    {
        return await _context.PostIterations.FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public async Task<PostIteration> GetNewest()
    {
        return await _context.PostIterations.OrderByDescending(x => x.StartDateTime).FirstOrDefaultAsync();
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