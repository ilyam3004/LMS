namespace Contracts.Responses.Subjects;

public record StudentSubjectResponse(
    Guid Id,
    string Name,
    string Description,
    string LecturerName);