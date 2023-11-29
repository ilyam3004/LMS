using Contracts.Responses.Tasks;

namespace Contracts.Responses.Students;

public record StudentTasksResponse(
    Guid StudentId,
    string FullName,
    List<UploadedTaskResponse> Tasks);