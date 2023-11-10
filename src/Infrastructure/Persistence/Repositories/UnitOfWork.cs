using Application.Common.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork(LmsDbContext context) : IUnitOfWork
{
    private bool _disposed;

    public ILecturerRepository Lecturers { get; } = new LecturerRepository(context);
    public IUserRepository Users { get; } = new UserRepository(context);
    public ITaskRepository Tasks { get; } = new TaskRepository(context);

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }
}