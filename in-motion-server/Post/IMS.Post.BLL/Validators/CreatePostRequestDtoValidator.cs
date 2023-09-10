using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class CreatePostRequestDtoValidator : AbstractValidator<CreatePostRequestDto>
{
    public CreatePostRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2048);

        RuleFor(x => x.Localization)
            .NotEmpty();
    }
}