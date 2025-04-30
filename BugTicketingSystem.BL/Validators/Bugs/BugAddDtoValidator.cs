using BugTicketingSystem.BL;
using FluentValidation;

namespace BugTrackingSystem.BL;
public class BugAddDtoValidator : AbstractValidator<BugAddDTO>
{
    public BugAddDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Project name is required.")
            .MinimumLength(2)
            .WithMessage("Project name must be at least 2 characters long.")
            .Matches(@"^[a-zA-Z0-9\s]+$")
            .WithMessage("Project name can only contain letters, numbers, and spaces.");

        RuleFor(x => x.ProjectID)
            .NotEmpty()
            .WithMessage("Project ID is required.");

        RuleFor(x => x.Risk)
            .NotNull()
            .WithMessage("Risk name is required.")
            //.Must(name => new[] { "Low", "Normal", "Medium", "High", "Critical"  }.Contains(name))
            .IsInEnum()
            .WithMessage("Invalid project status. Status must be either Not Started, In Progress, Cancelled, or Completed.");


    }
}

