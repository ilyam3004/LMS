using Application.Models;
using MediatR;
using ErrorOr;

namespace Application.Authentication.Commands.RegisterStudent;

public record RegisterStudentCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string GroupName,
    int Course,
    DateTime Birthday,
    string Address) : IRequest<ErrorOr<AuthenticationResult>>;