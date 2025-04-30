using BugTicketingSystem.BL;
using FluentValidation;

namespace BugTrackingSystem.BL;
public class ProjectAddDtoValidator : AbstractValidator<ProjectAddDTO>
{
    public ProjectAddDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Project name is required.")
            .MinimumLength(2)
            .WithMessage("Project name must be at least 2 characters long.")
            .Matches(@"^[a-zA-Z0-9\s]+$")
            .WithMessage("Project name can only contain letters, numbers, and spaces.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid project status. Status must be either Not Started, In Progress, Cancelled, or Completed.");

    }
}

