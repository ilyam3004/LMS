namespace Application.Models.Tasks;

public record StudentTasksResult(
    Guid StudentId,
    string FullName,
    List<UploadedTaskResult> Tasks);