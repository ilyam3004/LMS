using Contracts.Responses.Tasks;

namespace Contracts.Responses.Subjects;

public record StudentSubjectResponse(
    Guid Id,
    string Name,
    string Description,
    string LecturerName,
    List<LecturerTaskResponse> Tasks);