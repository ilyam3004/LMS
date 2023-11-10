using Application.Common.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class TaskRepository : Repository<Task>, ITaskRepository 
{
    public TaskRepository(LmsDbContext context) : base(context)
    { }
}