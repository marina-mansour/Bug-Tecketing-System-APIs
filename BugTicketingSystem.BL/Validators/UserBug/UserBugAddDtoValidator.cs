using BugTicketingSystem.BL;
using FluentValidation;

namespace BugTrackingSystem.BL;
public class UserBugsAddDtoValidator : AbstractValidator<UserBugsAddDTO>
{
    public UserBugsAddDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");
    }
}

