using FluentValidation;
using FluentValidation.AspNetCore;
using IMS.Shared.Models.Dto.Auth;
using IMS.Shared.Models.Dto.Other;
using IMS.Shared.Models.Dto.Post;
using IMS.Shared.Models.Dto.Post.Comment;
using IMS.Shared.Models.Dto.Post.Reaction;
using IMS.Shared.Models.Validators.Auth;
using IMS.Shared.Models.Validators.Other;
using IMS.Shared.Models.Validators.Post;
using IMS.Shared.Models.Validators.Post.Comment;
using IMS.Shared.Models.Validators.Post.Reaction;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Shared.Models.Validators;

public static class LoadValidators
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IValidator<UserLoginWithEmailAndPasswordDto>, UserLoginWithEmailAndPasswordDtoValidator>();
        services.AddScoped<IValidator<UserRegisterWithEmailAndPasswordDto>, UserRegisterWithEmailAndPasswordDtoValidator>();

        services.AddScoped<IValidator<CreatePostDto>, CreatePostDtoValidator>();
        
        // Post comments
        services.AddScoped<IValidator<CreatePostCommentDto>, CreatePostCommentDtoValidator>();
        
        // Post reaction 
        services.AddScoped<IValidator<CreatePostReactionDto>, CreatePostReactionDtoValidator>();
        
        services.AddScoped<IValidator<LocalizationDto>, LocalizationDtoValidator>();
        services.AddScoped<IValidator<TagDto>, TagDtoValidator>();
        return services;
    }
}