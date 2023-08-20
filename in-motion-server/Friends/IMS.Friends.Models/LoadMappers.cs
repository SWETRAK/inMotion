using IMS.Friends.Models.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Friends.Models;

public static class LoadMappers
{
    public static IServiceCollection AddFriendsMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<FriendshipProfile>();
        });
        return serviceCollection;
    }
}