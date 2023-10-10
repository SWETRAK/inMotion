using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class UpdatePostCommentDtoValidator: AbstractValidator<UpdatePostCommentDto>
{
    public UpdatePostCommentDtoValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty();

        RuleFor(x => x.Content)
            .MaximumLength(1024)
            .NotEmpty();
    }
}