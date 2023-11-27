using Contracts.Responses.Students;

namespace Contracts.Responses.Tasks;

public record UploadedStudentTaskResponse(
    Guid StudentTaskId,
    Guid TaskId,
    string? FileUrl,
    DateTime? UploadedAt,
    int Grade,
    StudentTaskStatus Status,
    StudentResponse Student,
    List<TaskCommentResponse> Comments);