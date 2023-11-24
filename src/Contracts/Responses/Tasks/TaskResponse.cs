namespace Contracts.Responses.Tasks;

public record TaskResponse(
    Guid TaskId,
    string Title,
    string Description,
    DateTime Deadline,
    DateTime CreatedAt,
    int MaxGrade,
    string GroupName,
    List<UploadedTaskResponse> UploadedTasks);