using Application.Common.Interfaces.Persistence;
using Task = Domain.Entities.Task;

namespace Infrastructure.Persistence.Repositories;

public class TaskRepository : Repository<Task>, ITaskRepository 
{
    public TaskRepository(LmsDbContext context) : base(context)
    { }
}