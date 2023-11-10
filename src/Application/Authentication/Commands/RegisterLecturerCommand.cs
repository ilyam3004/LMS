using Application.Models;
using ErrorOr;

using MediatR;

namespace Application.Authentication.Commands;

public record RegisterLecturerCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? Degree,
    DateTime Birthday,
    string Address) : IRequest<ErrorOr<AuthenticationResult>>; 