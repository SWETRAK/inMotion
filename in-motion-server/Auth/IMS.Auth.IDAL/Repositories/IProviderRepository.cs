using IMS.Shared.Domain.Consts;
using IMS.Shared.Domain.Entities.User;

namespace IMS.Auth.IDAL.Repositories;

public interface IProviderRepository: IDisposable
{
    Task<Provider> GetByTokenAsync(Providers providerName, string token);
    Task<Provider> GetByTokenWithUserAsync(Providers providerName, string token);
    Task<Provider> GetByIdAsync(Guid providerId);
    Task Save(); 
}