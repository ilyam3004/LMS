namespace Contracts.Responses;

public record AuthenticationResponse(
    Guid UserId,
    string Email,
    string Token);