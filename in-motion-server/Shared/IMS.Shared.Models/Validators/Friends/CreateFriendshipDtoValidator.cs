using FluentValidation;
using IMS.Shared.Models.Dto.Friends;

namespace IMS.Shared.Models.Validators.Friends;

public class CreateFriendshipDtoValidator: AbstractValidator<CreateFriendshipDto>
{
    public CreateFriendshipDtoValidator()
    {
        RuleFor(x => x.SecondUserId)
            .NotEmpty();
    }
}