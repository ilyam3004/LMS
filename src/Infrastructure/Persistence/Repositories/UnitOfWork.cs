using Application.Common.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork(LmsDbContext context) : IUnitOfWork
{
    public IGroupRepository Groups { get; } = new GroupRepository(context);
    public IUserRepository Users { get; } = new UserRepository(context);
    public ITaskRepository Tasks { get; } = new TaskRepository(context);
    public ISubjectRepository Subjects => new SubjectRepository(context);

    private bool _disposed;

    public IRepository<T> GetRepository<T>() where T : class
        => new Repository<T>(context);

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