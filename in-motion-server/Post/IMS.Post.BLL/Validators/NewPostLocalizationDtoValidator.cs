using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class NewPostLocalizationDtoValidator: AbstractValidator<NewPostLocalizationDto>
{
    public NewPostLocalizationDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        
        RuleFor(x => x.Latitude)
            .NotEmpty();
        
        RuleFor(x => x.Longitude)
            .NotEmpty();
    }
}