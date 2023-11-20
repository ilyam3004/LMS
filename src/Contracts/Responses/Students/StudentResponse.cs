namespace Contracts.Responses.Students;

public record StudentResponse(
    Guid StudentId, 
    Guid UserId, 
    Guid GroupId, 
    string FullName, 
    DateTime Birthday, 
    string Address);