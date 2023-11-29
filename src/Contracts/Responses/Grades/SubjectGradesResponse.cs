using Contracts.Responses.Students;

namespace Contracts.Responses.Grades;

public record SubjectGradesResponse(
    Guid SubjectId,
    string SubjectName,
    string GroupName,
    List<StudentTasksResponse> StudentTasks);