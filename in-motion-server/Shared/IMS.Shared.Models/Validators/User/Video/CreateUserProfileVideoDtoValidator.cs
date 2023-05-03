using FluentValidation;
using IMS.Shared.Models.Dto.User.Video;

namespace IMS.Shared.Models.Validators.User.Video;

public class CreateUserProfileVideoDtoValidator: AbstractValidator<CreateUserProfileVideoDto>
{
    public CreateUserProfileVideoDtoValidator()
    {
        RuleFor(x => x.Filename)
            .NotEmpty();

        RuleFor(x => x.ContentType)
            .NotEmpty();
    }
}