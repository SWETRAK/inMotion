using IMS.Post.BLL.Services;
using IMS.Post.IBLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Post.BLL;

public static class LoadServices
{
    public static IServiceCollection AddPostServices(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddScoped<IPostService, PostService>();
        servicesCollection.AddScoped<IUserService, UserService>();
        
        
        return servicesCollection;
    }
}