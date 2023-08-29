using IMS.Friends.DAL.Repositories;
using IMS.Friends.Domain;
using IMS.Friends.IDAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Friends.DAL;

public static class LoadRepositories
{
    public static IServiceCollection AddFriendsRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ImsFriendsDbContext>();

        serviceCollection.AddScoped<IFriendshipRepository, FriendshipRepository>();

        return serviceCollection;
    }
}