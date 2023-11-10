namespace Application.Models;

public record AuthenticationResult(
    Guid UserId,
    string FullName, 
    string Email,
    string Token);