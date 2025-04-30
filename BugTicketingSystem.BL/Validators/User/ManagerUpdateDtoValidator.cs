
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using FluentValidation;

namespace BugTrackingSystem.BL;

public class ManagerUpdateDtoValidator : AbstractValidator<UserManagerUpdateDTO>
{
    private readonly IUnitOfWork _unitWork;
    public ManagerUpdateDtoValidator(
        IUnitOfWork unitWork
        )
    {
        _unitWork = unitWork;
        //Rules
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(2)
            .WithMessage("Name must be at least 2 characters long.")
            .Matches(@"^[a-zA-Z]+$")
            .WithMessage("Name must contain only letters.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MustAsync(CheckEmailUniqueness)
            .WithMessage("Email already exists.")
            .When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.Salary)
            .GreaterThanOrEqualTo(5000)
            .WithMessage("Salary must be more than or equal 5000!");
        RuleFor(x => x.Age)
            .GreaterThan(18)
            .WithMessage("Cannot be less than 18 years old.");
        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Role must be one of the predefined values: Manager, Developer, Tester.");
    }

    private async Task<bool> CheckEmailUniqueness(
    string email,
    CancellationToken token)
    {
        var users = await _unitWork.UserRepository.getAllAsync();
        return users.All(u => u.Email != email);
    }
}
