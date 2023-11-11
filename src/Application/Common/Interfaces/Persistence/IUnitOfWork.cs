namespace Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ITaskRepository Tasks { get; }
    ILecturerRepository Lecturers { get; }
    IStudentRepository Students { get; }
    IGroupRepository Groups { get; }
    IRepository<T> GetRepository<T>() where T : class;
    Task<int> SaveChangesAsync();
    void Dispose();
}