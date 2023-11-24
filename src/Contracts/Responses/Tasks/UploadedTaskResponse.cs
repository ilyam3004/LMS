namespace Contracts.Responses.Tasks;

public record UploadedTaskResponse(
    Guid UploadedTaskId,
    Guid TaskId,
    string FileUrl,
    DateTime UploadedAt,
    int Grade,
    Guid StudentId,
    string StudentFullName);