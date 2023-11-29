namespace Contracts.Responses.Tasks;

public record LecturerTaskResponse(
    Guid TaskId,
    string Title,
    string Description,
    DateTime Deadline,
    DateTime CreatedAt,
    int MaxGrade,
    string GroupName,
    List<UploadedTaskResponse> StudentTasks);