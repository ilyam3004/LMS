using Application.Models.Authentication;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Authentication.Commands.RegisterLecturer;

public record RegisterLecturerCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? Degree,
    DateTime Birthday,
    string Address) : IRequest<Result<AuthenticationResult>>; 