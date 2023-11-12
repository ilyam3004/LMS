using FluentValidation;

namespace Application.Authentication.Commands.RegisterLecturer;

public class RegisterLecturerCommandValidator
    : AbstractValidator<RegisterLecturerCommand>
{
    public RegisterLecturerCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Birthday).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}