using Contracts.Responses.Groups;

namespace Contracts.Responses.Subjects;

public record LecturerSubjectResponse(
    Guid SubjectId,
    string Name,
    string Description,
    List<GroupResponse> Groups);