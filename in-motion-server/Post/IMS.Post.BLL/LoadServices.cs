using IMS.Post.BLL.RabbitConsumers;
using IMS.Post.BLL.Services;
using IMS.Post.IBLL.Services;
using IMS.Shared.Messaging;
using IMS.Shared.Messaging.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Post.BLL;

public static class LoadServices
{
    public static IServiceCollection AddPostServices(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddScoped<IPostService, PostService>();
        servicesCollection.AddScoped<IPostReactionService, PostReactionService>();
        servicesCollection.AddScoped<IPostCommentService, PostCommentService>();
        servicesCollection.AddScoped<IPostVideoService, PostVideoService>();
        
        servicesCollection.AddScoped<IConsumer, SaveUploadedVideoRabbitConsumer>();
        servicesCollection.AddCustomRabbit();
        
        servicesCollection.AddScoped<IUserService, UserService>();
        
        return servicesCollection;
    }
}