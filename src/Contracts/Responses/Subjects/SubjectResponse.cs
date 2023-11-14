namespace Contracts.Responses.Subjects;

public record SubjectResponse(
    Guid Id,
    string Name,
    string Description,
    string GroupName);