using IMS.Friends.BLL.Services;
using IMS.Friends.IBLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Friends.BLL;

public static class LoadServices
{
    public static IServiceCollection AddFriendsServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IFriendsListsService, FriendsListsService>();
        serviceCollection.AddScoped<IFriendsService, FriendsService>();
        
        serviceCollection.AddScoped<IUserService, UserService>();
        
        return serviceCollection;
    }
}