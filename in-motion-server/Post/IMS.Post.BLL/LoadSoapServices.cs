using IMS.Post.BLL.SoapServices;
using IMS.Post.IBLL.SoapServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace IMS.Post.BLL;

public static class LoadSoapServices
{
    public static IServiceCollection AddPostSoapService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPostVideoSoapService, PostVideoSoapService>();
        return serviceCollection;
    }

    public static void UsePostSoapService(this WebApplication webApplication)
    {
        webApplication.UseSoapEndpoint<IPostVideoSoapService>("/soap/postVideoService.asmx", new SoapEncoderOptions());
    }
}