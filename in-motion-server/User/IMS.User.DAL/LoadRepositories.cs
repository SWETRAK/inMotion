using IMS.User.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.User.DAL;

public static class LoadRepositories
{
    public static IServiceCollection AddFriendsRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ImsUserDbContext>();


        return serviceCollection;
    }
}