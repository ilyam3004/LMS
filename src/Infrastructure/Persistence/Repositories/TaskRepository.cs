using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Task;

namespace Infrastructure.Persistence.Repositories;

public class TaskRepository : Repository<Task>, ITaskRepository 
{
    public TaskRepository(LmsDbContext context) : base(context)
    { }

    public async Task<Task?> GetTaskByIdWithRelations(Guid taskId)
        => await DbContext.Tasks
            .Include(t => t.StudentTasks)
            .Include(t => t.Subject)
            .ThenInclude(s => s.Lecturer)
            .FirstOrDefaultAsync(t => t.TaskId == taskId);
}