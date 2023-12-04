using Application.Models.Authentication;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Authentication.Commands.RegisterStudent;

public record RegisterStudentCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string GroupName,
    int Course,
    DateTime Birthday,
    string Address) : IRequest<Result<AuthenticationResult>>;