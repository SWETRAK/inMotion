using IMS.Post.Models.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Post.Models;

public static class LoadMappers
{
    public static IServiceCollection AddPostMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<PostProfile>();
            cfg.AddProfile<PostReactionProfile>();
            cfg.AddProfile<PostVideoProfile>();
            cfg.AddProfile<PostCommentProfile>();
            
            cfg.AddProfile<AuthorProfile>();
        });
        
        return serviceCollection;
    }
}