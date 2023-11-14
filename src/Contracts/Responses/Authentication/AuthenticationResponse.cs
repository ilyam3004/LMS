namespace Contracts.Responses.Authentication;

public record AuthenticationResponse(
    Guid UserId,
    string Email,
    string Token);