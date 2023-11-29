namespace Contracts.Responses.Tasks;

public record StudentTaskResponse(
    Guid TaskId,
    string Title,
    string Description,
    DateTime Deadline,
    DateTime CreatedAt,
    int MaxGrade,
    string LecturerName,
    UploadedTaskResponse UploadedTask);