using IMS.Post.Domain;
using IMS.Post.IDAL.Repositories.Other;

namespace IMS.Post.DAL.Repositories.Other;

public class LocalizationRepository: ILocalizationRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public LocalizationRepository(ImsPostDbContext context)
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