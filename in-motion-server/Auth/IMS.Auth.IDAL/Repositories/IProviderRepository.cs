using IMS.Auth.Domain.Consts;
using IMS.Auth.Domain.Entities;

namespace IMS.Auth.IDAL.Repositories;

public interface IProviderRepository: IDisposable
{
    Task<Provider> GetByTokenAsync(Providers providerName, string token);
    Task<Provider> GetByTokenWithUserAsync(Providers providerName, string token);
    Task<Provider> GetByIdAsync(Guid providerId);
    Task Insert(Provider provider);
    Task Save(); 
}