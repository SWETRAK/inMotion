using FluentValidation;
using IMS.Shared.Models.Dto.User.Video.Reaction;

namespace IMS.Shared.Models.Validators.User.Video.Reaction;

public class CreateUserProfileVideoReactionDtoValidator: AbstractValidator<CreateUserProfileVideoReactionDto>
{
    public CreateUserProfileVideoReactionDtoValidator()
    {
        RuleFor(x => x.Emoji)
            .NotEmpty();

        RuleFor(x => x.ProfileVideoId)
            .NotEmpty();
    }
}