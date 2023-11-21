using Application.Common.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsDbContext _context;
    private bool _disposed;

    public UnitOfWork(LmsDbContext context)
    {
        _context = context;
        Groups = new GroupRepository(_context);
        Users = new UserRepository(_context);
        Tasks = new TaskRepository(_context);
        Subjects = new SubjectRepository(_context);
    }

    public IUserRepository Users { get; }
    public ITaskRepository Tasks { get; }
    public ISubjectRepository Subjects { get; }
    public IGroupRepository Groups { get; }

    public IRepository<T> GetRepository<T>() where T : class
        => new Repository<T>(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
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
            _context.Dispose();
        }

        _disposed = true;
    }
}