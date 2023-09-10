using IMS.Post.Domain;
using IMS.Post.Domain.Entities.Other;
using IMS.Post.IDAL.Repositories.Other;
using Microsoft.EntityFrameworkCore;

namespace IMS.Post.DAL.Repositories.Other;

public class LocalizationRepository: ILocalizationRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public LocalizationRepository(ImsPostDbContext context)
    {
        _context = context;
    }


    public async Task<Localization> GetByCoordinatesOrNameAsync(double latitude, double longitude, string name)
    {
        return await _context.Localizations.FirstOrDefaultAsync(x =>
            (x.Latitude.Equals(latitude) && x.Longitude.Equals(longitude)) || x.Name.ToLower().Equals(name.ToLower()));
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