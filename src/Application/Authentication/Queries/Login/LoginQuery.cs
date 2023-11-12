using Application.Models;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email, 
    string Password) : IRequest<Result<AuthenticationResult>>;