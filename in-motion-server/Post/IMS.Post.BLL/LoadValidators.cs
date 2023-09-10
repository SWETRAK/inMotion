using FluentValidation;
using FluentValidation.AspNetCore;
using IMS.Post.BLL.Validators;
using IMS.Post.Models.Dto.Incoming;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Post.BLL;

public static  class LoadValidators
{
    public static IServiceCollection AddPostValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation();

        serviceCollection.AddScoped<IValidator<CreatePostRequestDto>, CreatePostRequestDtoValidator>();
        
        return serviceCollection;
    }
}