using Task = Domain.Entities.Task;

namespace Application.Common.Interfaces.Persistence;

public interface ITaskRepository : IRepository<Task>
{
    Task<Task?> GetTaskByIdWithRelations(Guid taskId);
}