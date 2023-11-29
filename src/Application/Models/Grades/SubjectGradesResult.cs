using Application.Models.Tasks;

namespace Application.Models.Grades;

public record SubjectGradesResult(
    Guid SubjectId,
    string SubjectName,
    string GroupName,
    List<StudentTasksResult> StudentTasks);