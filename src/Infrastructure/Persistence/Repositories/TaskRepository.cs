﻿using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Task;

namespace Infrastructure.Persistence.Repositories;

public class TaskRepository : Repository<Task>, ITaskRepository
{
    public TaskRepository(LmsDbContext context) : base(context)
    {
    }

    public async Task<Task?> GetTaskByIdWithRelations(Guid taskId)
        => await DbContext.Tasks
            .Include(t => t.StudentTasks)
            .ThenInclude(st => st.Comments)
            .ThenInclude(c => c.User)
            .Include(t => t.StudentTasks)
            .ThenInclude(st => st.Student)
            .Include(t => t.Subject)
            .ThenInclude(s => s.Group)
            .Include(t => t.Subject)
            .ThenInclude(s => s.Lecturer)
            .FirstOrDefaultAsync(t => t.TaskId == taskId);

    public async Task<bool> TaskExists(Guid taskId)
        => await DbContext.Tasks.AnyAsync(t => t.TaskId == taskId);
}