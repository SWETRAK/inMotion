using IMS.Post.Domain.Entities.Other;
using Microsoft.EntityFrameworkCore.Storage;

namespace IMS.Post.IDAL.Repositories.Other;

public interface ITagRepository: IDisposable
{
    Task<IList<Tag>> GetByNamesAsync(IEnumerable<string> names);

    Task<IDbContextTransaction> StartTransactionAsync();

    Task SaveAsync();
}