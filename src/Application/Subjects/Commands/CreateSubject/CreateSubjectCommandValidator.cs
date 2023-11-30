using FluentValidation;

namespace Application.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandValidator 
    : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(command => command.GroupName)
            .NotEmpty();

        RuleFor(command => command.Token)
            .NotEmpty()
            .NotNull();
    }
}