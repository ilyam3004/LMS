namespace Contracts.Responses.Subjects;

public record LecturerSubjectResponse(
    Guid Id,
    string Name,
    string Description,
    string GroupName);