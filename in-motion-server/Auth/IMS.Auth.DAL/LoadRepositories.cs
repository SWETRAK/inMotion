using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.DAL;

public static class LoadRepositories
{
    public static IServiceCollection AddAuthRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}