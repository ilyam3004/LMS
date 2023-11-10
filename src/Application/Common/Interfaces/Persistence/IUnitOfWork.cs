namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ITaskRepository Tasks { get; }
    ILecturerRepository Lecturers { get; }

    Task<int> SaveChangesAsync();
    void Dispose();
}