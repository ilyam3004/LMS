namespace Contracts.Requests.Authentication;

public record LoginRequest(
    string Email,
    string Password);