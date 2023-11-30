using Contracts.Responses.Tasks;

namespace Contracts.Responses.Students;

public record StudentTasksResponse(
    Guid StudentId,
    string FullName,
    int TotalGrade,
    double AverageGrade,
    List<StudentTaskResponse> Tasks);