using IMS.User.BLL.Services;
using IMS.User.IBLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.User.BLL;

public static class LoadServices
{
    public static IServiceCollection AddUserServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IUserProfileVideoService, UserProfileVideoService>();
        
        return serviceCollection;
    }
}