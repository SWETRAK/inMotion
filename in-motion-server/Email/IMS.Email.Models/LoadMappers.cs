using IMS.Email.Models.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Email.Models;

public static class LoadMappers
{
    public static IServiceCollection AddEmailMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<AuthEmailProfile>();
        });
        return serviceCollection;
    }
}