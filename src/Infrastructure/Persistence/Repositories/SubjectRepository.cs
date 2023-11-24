using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(LmsDbContext context) : base(context)
    {
    }

    public async Task<List<Subject>> GetLecturerSubjects(Guid lecturerId)
        => await DbContext.Subjects.Where(s => s.LecturerId == lecturerId)
            .Include(s => s.Group)
            .ThenInclude(g => g.Students)
            .Include(s => s.Tasks)
            .ToListAsync();

    public async Task<List<Subject>> GetStudentSubjects(Guid groupId)
        => await DbContext.Subjects
            .Where(gs => gs.GroupId == groupId)
            .Include(s => s.Lecturer)
            .ToListAsync();

    public async Task<bool> SubjectExists(Guid subjectId)
        => await DbContext.Subjects.AnyAsync(s => s.SubjectId == subjectId);

    public async Task<Subject?> GetSubjectByIdWithRelations(Guid subjectId)
        => await DbContext.Subjects
            .Include(gs => gs.Group)
            .ThenInclude(g => g.Students)
            .Include(s => s.Lecturer)
            .Include(s => s.Tasks)
            .ThenInclude(t => t.StudentTasks)
            .FirstOrDefaultAsync(s => s.SubjectId == subjectId);
}