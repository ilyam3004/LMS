using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class StudentTaskRepository : Repository<StudentTask>,
    IStudentTaskRepository
{
    public StudentTaskRepository(LmsDbContext context) : base(context)
    { }

    public async Task<StudentTask?> GetByIdAsyncWithRelations(Guid studentTaskId)
        => await DbContext.StudentTasks
            .Include(t => t.Task)
            .Include(t => t.Student)
            .FirstOrDefaultAsync(s => s.StudentTaskId == studentTaskId);
}