using FluentValidation;
using FluentValidation.AspNetCore;
using IMS.Shared.Models.Dto.Auth;
using IMS.Shared.Models.Dto.Other;
using IMS.Shared.Models.Dto.Post;
using IMS.Shared.Models.Dto.Post.Comment;
using IMS.Shared.Models.Dto.Post.Comment.Reaction;
using IMS.Shared.Models.Dto.Post.Reaction;
using IMS.Shared.Models.Dto.User.Video;
using IMS.Shared.Models.Dto.User.Video.Reaction;
using IMS.Shared.Models.Validators.Auth;
using IMS.Shared.Models.Validators.Other;
using IMS.Shared.Models.Validators.Post;
using IMS.Shared.Models.Validators.Post.Comment;
using IMS.Shared.Models.Validators.Post.Comment.Reaction;
using IMS.Shared.Models.Validators.Post.Reaction;
using IMS.Shared.Models.Validators.User.Video;
using IMS.Shared.Models.Validators.User.Video.Reaction;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Shared.Models.Validators;

public static class LoadValidators
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        
        services.AddScoped<IValidator<UserLoginWithEmailAndPasswordDto>, UserLoginWithEmailAndPasswordDtoValidator>();
        services.AddScoped<IValidator<UserRegisterWithEmailAndPasswordDto>, UserRegisterWithEmailAndPasswordDtoValidator>();

        // User profile video 
        services.AddScoped<IValidator<CreateUserProfileVideoDto>, CreateUserProfileVideoDtoValidator>();
        
        //User profile video reaction
        services.AddScoped<IValidator<CreateUserProfileVideoReactionDto>, CreateUserProfileVideoReactionDtoValidator>();
        
        // Post
        services.AddScoped<IValidator<CreatePostDto>, CreatePostDtoValidator>();
        
        // Post comments
        services.AddScoped<IValidator<CreatePostCommentDto>, CreatePostCommentDtoValidator>();
        services.AddScoped<IValidator<CreatePostCommentReactionDto>, CreatePostCommentReactionDtoValidator>();
        
        // Post reaction 
        services.AddScoped<IValidator<CreatePostReactionDto>, CreatePostReactionDtoValidator>();
        
        services.AddScoped<IValidator<LocalizationDto>, LocalizationDtoValidator>();
        services.AddScoped<IValidator<TagDto>, TagDtoValidator>();
        return services;
    }
}