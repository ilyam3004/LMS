using Domain.Abstractions.Results;
using Application.Models;
using Application.Models.Authentication;
using MediatR;

namespace Application.Authentication.Commands.RegisterLecturer;

public record RegisterLecturerCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? Degree,
    DateTime Birthday,
    string Address) : IRequest<Result<AuthenticationResult>>; 