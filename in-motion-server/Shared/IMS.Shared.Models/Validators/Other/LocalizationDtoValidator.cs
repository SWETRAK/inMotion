using FluentValidation;
using IMS.Shared.Models.Dto.Other;

namespace IMS.Shared.Models.Validators.Other;

public class LocalizationDtoValidator: AbstractValidator<LocalizationDto>
{
    public LocalizationDtoValidator()
    {
        RuleFor(x => x.Longitude)
            .NotEmpty();

        RuleFor(x => x.Latitude)
            .NotNull();
    }
}