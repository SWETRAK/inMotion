using IMS.Auth.Domain;
using IMS.Auth.Domain.Consts;
using IMS.Auth.Domain.Entities;
using IMS.Auth.IDAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMS.Auth.DAL.Repositories;

public class ProviderRepository: IProviderRepository
{
    private readonly ImsAuthDbContext _context;
    private bool _disposed = false;

    public ProviderRepository(ImsAuthDbContext context)
    {
        _context = context;
    }

    public async Task<Provider> GetByTokenAsync(Providers providerName, string token)
    {
        return await _context
            .Providers
            .FirstOrDefaultAsync(x => x.AuthKey == token && x.Name == providerName); 
    }

    public async Task<Provider> GetByTokenWithUserAsync(Providers providerName, string token)
    {
        return await _context
            .Providers
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.AuthKey == token && x.Name == providerName);
    }

    public async Task<Provider> GetByIdAsync(Guid providerId)
    {
        return await _context
            .Providers
            .FirstOrDefaultAsync(x => x.Id == providerId);
    }
    
    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this._disposed = true;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}