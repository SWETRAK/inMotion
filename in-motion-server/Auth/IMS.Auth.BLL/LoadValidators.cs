using FluentValidation;
using FluentValidation.AspNetCore;
using IMS.Auth.BLL.Validators;
using IMS.Auth.Models.Dto.Incoming;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Auth.BLL;

public static class LoadValidators
{
    public static IServiceCollection AddAuthValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidationAutoValidation();

        serviceCollection.AddScoped<IValidator<LoginUserWithEmailAndPasswordDto>, LoginUserWithEmailAndPasswordDtoValidator>();
        serviceCollection.AddScoped<IValidator<RegisterUserWithEmailAndPasswordDto>, RegisterUserWithEmailAndPasswordDtoValidator>();

        serviceCollection.AddScoped<IValidator<AuthenticateWithFacebookProviderDto>, AuthenticateWithFacebookProviderDtoValidator>();
        serviceCollection.AddScoped<IValidator<AuthenticateWithGoogleProviderDto>, AuthenticateWithGoogleProviderDtoValidator>();

        serviceCollection.AddScoped<IValidator<AddPasswordDto>, AddPasswordDtoValidator>();
        serviceCollection.AddScoped<IValidator<UpdateEmailDto>, UpdateEmailDtoValidator>();
        serviceCollection.AddScoped<IValidator<UpdateNicknameDto>, UpdateNicknameDtoValidator>();
        serviceCollection.AddScoped<IValidator<UpdatePasswordDto>, UpdatePasswordDtoValidator>();
        
        return serviceCollection;
    }
}