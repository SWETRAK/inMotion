using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class VideoMetaDataDtoValidator: AbstractValidator<VideoMetaDataDto>
{
    public VideoMetaDataDtoValidator()
    {
        
        RuleFor(x => x.BucketLocation)
            .NotEmpty();

        RuleFor(x => x.BucketName)
            .NotEmpty();
        
        RuleFor(x => x.Filename)
            .NotEmpty();

        RuleFor(x => x.ContentType)
            .NotEmpty();

        // TODO: Check if this rule works
        RuleFor(x => x.Type)
            .IsInEnum()
            .NotNull();
    }
}