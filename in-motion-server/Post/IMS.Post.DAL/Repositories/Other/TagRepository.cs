using System.Transactions;
using IMS.Post.Domain;
using IMS.Post.Domain.Entities.Other;
using IMS.Post.IDAL.Repositories.Other;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IMS.Post.DAL.Repositories.Other;

public class TagRepository: ITagRepository
{
    private readonly ImsPostDbContext _context;
    private bool _disposed = false;

    public TagRepository(ImsPostDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Tag>> GetByNamesAsync(IEnumerable<string> names)
    {
        var lowerNames = names.Select(x => x.ToLower());
        return await _context.Tags.Where(x => lowerNames.Contains(x.Name.ToLower())).ToListAsync();
    }


    public async Task<IDbContextTransaction> StartTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
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