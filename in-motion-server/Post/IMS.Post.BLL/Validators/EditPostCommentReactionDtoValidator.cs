using FluentValidation;
using IMS.Post.Models.Dto.Incoming;

namespace IMS.Post.BLL.Validators;

public class EditPostCommentReactionDtoValidator: AbstractValidator<EditPostCommentReactionDto>
{
    public EditPostCommentReactionDtoValidator()
    {
        RuleFor(x => x.PostCommentId).NotEmpty();
        RuleFor(x => x.Emoji).NotEmpty();
    }
}