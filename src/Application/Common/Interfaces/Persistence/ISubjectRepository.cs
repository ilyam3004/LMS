using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ISubjectRepository : IRepository<Subject>
{
    Task<bool> ExistsByName(string name);
    Task<List<Subject>> GetLecturerSubjects(Guid lecturerId);
}