using FluentValidation;
using IMS.Shared.Models.Dto.Post;

namespace IMS.Shared.Models.Validators.Post;

public class CreatePostDtoValidator: AbstractValidator<CreatePostDto>
{
    public CreatePostDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(256)
            .NotNull();

        RuleFor(x => x.Description)
            .MaximumLength(2048);
    }
}