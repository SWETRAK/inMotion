using FluentValidation;
using IMS.Shared.Models.Dto.Other;

namespace IMS.Shared.Models.Validators.Other;

public class TagDtoValidator: AbstractValidator<TagDto>
{
    public TagDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .MaximumLength(24);
    }
}