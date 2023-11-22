using Contracts.Responses.Groups;
using Contracts.Responses.Tasks;

namespace Contracts.Responses.Subjects;

public record LecturerSubjectResponse(
    Guid SubjectId,
    string Name,
    string Description,
    List<GroupResponse> Groups,
    List<TaskResponse> Tasks);