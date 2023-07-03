using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.DAL;

public static class LoadMappers
{
    public static IServiceCollection AddAuthMappers(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}