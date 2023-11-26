using Task = Domain.Entities.Task;

namespace Application.Common.Interfaces.Persistence;

public interface ITaskRepository : IRepository<Task>
{
    Task<Task?> GetTaskByIdWithGroupRelation(Guid taskId);
    Task<Task?> GetTaskByIdWithLecturerRelation(Guid taskId);
    Task<bool> TaskExists(Guid taskId);
}