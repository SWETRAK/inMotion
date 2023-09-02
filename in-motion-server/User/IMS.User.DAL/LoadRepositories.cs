using IMS.User.DAL.Repositories;
using IMS.User.Domain;
using IMS.User.IDAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.User.DAL;

public static class LoadRepositories
{
    public static IServiceCollection AddUserRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ImsUserDbContext>();

        serviceCollection.AddScoped<IUserMetasRepository, UserMetasRepository>();
        serviceCollection.AddScoped<IUserProfileVideoRepository, UserProfileVideoRepository>();

        return serviceCollection;
    }
}
