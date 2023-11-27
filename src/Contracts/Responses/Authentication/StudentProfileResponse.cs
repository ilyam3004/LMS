namespace Contracts.Responses.Authentication;

public record StudentProfileResponse(
    Guid UserId,
    string Email,
    string FullName,
    string Group,
    string Course,
    string Birthday,
    string Address);