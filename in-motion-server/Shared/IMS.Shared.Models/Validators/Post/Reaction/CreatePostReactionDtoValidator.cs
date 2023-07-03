using FluentValidation;
using IMS.Shared.Models.Dto.Post.Reaction;

namespace IMS.Shared.Models.Validators.Post.Reaction;

public class CreatePostReactionDtoValidator: AbstractValidator<CreatePostReactionDto>
{
    public CreatePostReactionDtoValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull();

        RuleFor(x => x.Emoji)
            .NotNull();
    }
}