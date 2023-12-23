using IMS.Post.DAL.Repositories.Post;
using IMS.Post.Domain;
using IMS.Post.IDAL.Repositories.Post;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Post.DAL;

public static class LoadRepositories
{
    public static IServiceCollection AddPostRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContextFactory<ImsPostDbContext>();
        serviceCollection.AddDbContext<ImsPostDbContext>();
        
        serviceCollection.AddScoped<IPostIterationRepository, PostIterationRepository>();
        serviceCollection.AddScoped<IPostRepository, PostRepository>();
        serviceCollection.AddScoped<IPostCommentRepository, PostCommentRepository>();
        serviceCollection.AddScoped<IPostReactionRepository, PostReactionRepository>();
        serviceCollection.AddScoped<IPostVideoRepository, PostVideoRepository>();
        
        return serviceCollection;
    }
}