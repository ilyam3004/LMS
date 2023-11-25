using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IStudentTaskRepository : IRepository<StudentTask>
{
    Task<StudentTask?> GetByIdAsyncWithRelations(Guid studentTaskId);
}