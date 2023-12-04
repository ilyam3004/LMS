using FluentValidation;

namespace Application.Authentication.Queries.Login;

public class LoginQueryValidator: AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
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
            .WithMessage("Password should contain letters and numbers.");   
    }
}