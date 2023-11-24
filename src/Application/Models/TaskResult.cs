using Task = Domain.Entities.Task;

namespace Application.Models;

public record TaskResult(
    Task Task, 
    List<StudentTaskResult> StudentTasks,
    string GroupName);