using FluentValidation;

namespace Application.Features.Tasks.Commands.CreateTask;

public class AssignTaskCommandValidator : AbstractValidator<AssignTaskCommand>
{
    public AssignTaskCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Description)
            .NotEmpty()
            .MaximumLength(2000);
        
        RuleFor(command => command.MaxGrade)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(101)
            .WithMessage("The maximum grade must be between 1 and 100");

        RuleFor(command => command.SubjectId)
            .NotEmpty();
    }
}