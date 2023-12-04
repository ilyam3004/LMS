using FluentValidation;

namespace Application.Features.Authentication.Commands.RegisterLecturer;

public class RegisterLecturerCommandValidator
    : AbstractValidator<RegisterLecturerCommand>
{
    public RegisterLecturerCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(319);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(8, 64)
            .WithMessage("Password should be at least 8 characters long and at most 64 characters long.")
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
            .WithMessage("Password should contain only letters and numbers.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Birthday)
            .NotEmpty();

        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(200);
    }
}