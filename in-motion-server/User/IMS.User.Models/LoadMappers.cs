using IMS.User.Models.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.User.Models;

public static class LoadMappers
{
    public static IServiceCollection AddUserMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserMetasProfile>();
            cfg.AddProfile<UserProfileVideoProfile>();
        });
        return serviceCollection;
    }
}
