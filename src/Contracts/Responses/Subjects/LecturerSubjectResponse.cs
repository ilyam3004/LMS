using Contracts.Responses.Groups;
using Contracts.Responses.Tasks;

namespace Contracts.Responses.Subjects;

public record LecturerSubjectResponse(
    Guid SubjectId,
    string Name,
    string Description,
    GroupResponse Group,
    List<LecturerTaskResponse> Tasks);