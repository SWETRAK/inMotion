using IMS.Auth.DAL.Repositories;
using IMS.Auth.IDAL.Repositories;
using IMS.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.DAL;

public static class LoadRepositories
{
    public static IServiceCollection AddAuthRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ImsDbContext>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IProviderRepository, ProviderRepository>();
        return serviceCollection;
    }
}