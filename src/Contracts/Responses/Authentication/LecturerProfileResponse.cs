namespace Contracts.Responses.Authentication;

public record LecturerProfileResponse(
    Guid UserId,
    string Email, 
    string FullName,
    string Degree, 
    string BirthDate,
    string Address);