namespace Contracts.Responses;

public record AuthenticationResponse(
    Guid UserId,
    string FullName, 
    string Email,
    string Token);