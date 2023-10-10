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
        serviceCollection.AddScoped<IValidator<CreatePostCommentDto>, CreatePostCommentDtoValidator>();
        serviceCollection.AddScoped<IValidator<CreatePostReactionDto>, CreatePostReactionDtoValidator>();
        serviceCollection.AddScoped<IValidator<CreatePostCommentReactionDto>, CreatePostCommentReactionDtoValidator>();
        
        serviceCollection.AddScoped<IValidator<EditPostCommentReactionDto>, EditPostCommentReactionDtoValidator>();
        serviceCollection.AddScoped<IValidator<EditPostReactionDto>, EditPostReactionDtoValidator>();
        serviceCollection.AddScoped<IValidator<EditPostRequestDto>, EditPostRequestDtoValidator>();

        serviceCollection.AddScoped<IValidator<NewPostLocalizationDto>, NewPostLocalizationDtoValidator>();
        serviceCollection.AddScoped<IValidator<UpdatePostCommentDto>, UpdatePostCommentDtoValidator>();
        serviceCollection.AddScoped<IValidator<UploadVideosMetaDataDto>, UploadVideoMetaDataDtoValidator>();
        serviceCollection.AddScoped<IValidator<VideoMetaDataDto>, VideoMetaDataDtoValidator>();
        
        return serviceCollection;
    }
}