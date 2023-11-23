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
            .Include(s => s.GroupSubjects)
            .ThenInclude(gs => gs.Group)
            .ThenInclude(g => g.Students)
            .Include(s => s.Tasks)
            .ToListAsync();

    public async Task<List<Subject>> GetStudentSubjects(Guid groupId)
        => await DbContext.GroupSubjects
            .Where(gs => gs.GroupId == groupId)
            .Include(gs => gs.Subject)
            .ThenInclude(s => s.Lecturer)
            .Select(gs => gs.Subject)
            .ToListAsync();

    public async Task<bool> SubjectExists(Guid subjectId)
        => await DbContext.Subjects.AnyAsync(s => s.SubjectId == subjectId);

    public async Task<Subject> GetSubjectByIdWithRelations(Guid subjectId)
        => await DbContext.Subjects
            .Include(s => s.GroupSubjects)
            .ThenInclude(gs => gs.Group)
            .ThenInclude(g => g.Students)
            .Include(s => s.Lecturer)
            .Include(s => s.Tasks)
            .FirstOrDefaultAsync(s => s.SubjectId == subjectId);
}