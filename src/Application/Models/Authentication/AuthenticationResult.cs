using Domain.Entities;

namespace Application.Models.Authentication;

public record AuthenticationResult(
    User User, 
    string Token);