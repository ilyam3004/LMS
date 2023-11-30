namespace Application.Models.Tasks;

public record StudentTasksResult(
    Guid StudentId,
    string FullName,
    int TotalGrade,
    double AverageGrade,
    List<StudentTaskResult> Tasks);