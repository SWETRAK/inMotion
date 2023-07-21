using IMS.Auth.DAL.Repositories;
using IMS.Auth.Domain;
using IMS.Auth.IDAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.DAL;

public static class LoadRepositories
{
    public static IServiceCollection AddAuthRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ImsAuthDbContext>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IProviderRepository, ProviderRepository>();
        return serviceCollection;
    }
}