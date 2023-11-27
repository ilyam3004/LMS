namespace Contracts.Responses.Authentication;

public record LecturerProfileResponse(
    Guid UserId,
    string Email, 
    string FullName,
    string Degree, 
    string Birthday,
    string Address);