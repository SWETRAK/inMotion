using IMS.Auth.Models.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.Models;

public static class LoadMappers
{
    public static IServiceCollection AddAuthMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserProfile>();
        });
        return serviceCollection;
    }
}