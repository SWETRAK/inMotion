using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class UploadVideoMetaDataDtoValidator: AbstractValidator<UploadVideosMetaDataDto>
{
    public UploadVideoMetaDataDtoValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty();

        RuleFor(x => x.AuthorId)
            .NotEmpty();

        RuleFor(x => x.VideosMetaData)
            .Must(x => x.Count().Equals(2))
            .NotEmpty();
    }
}