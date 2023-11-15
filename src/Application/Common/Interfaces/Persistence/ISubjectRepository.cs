using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ISubjectRepository : IRepository<Subject>
{
    Task<List<Subject>> GetLecturerSubjects(Guid lecturerId);
    Task<List<Subject>> GetStudentSubjects(Guid groupId);
    Task<bool> SubjectExists(Guid subjectId);
}