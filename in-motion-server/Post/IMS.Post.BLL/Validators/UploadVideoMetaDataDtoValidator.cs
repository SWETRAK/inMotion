using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class UploadVideoMetaDataDtoValidator: AbstractValidator<UploadVideoMetaDataDto>
{
    public UploadVideoMetaDataDtoValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty();

        RuleFor(x => x.BucketLocation)
            .NotEmpty();

        RuleFor(x => x.BucketName)
            .NotEmpty();
        
        RuleFor(x => x.Filename)
            .NotEmpty();

        RuleFor(x => x.AuthorId)
            .NotEmpty();

        RuleFor(x => x.ContentType)
            .NotEmpty();

        // TODO: Check if this rule works
        RuleFor(x => x.Type)
            .IsInEnum()
            .NotNull();
    }
}