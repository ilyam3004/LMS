using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class SubjectRepository(LmsDbContext context) :
    Repository<Subject>(context), ISubjectRepository
{
    public async Task<List<Subject>> GetLecturerSubjects(Guid lecturerId)
        => await DbContext.Subjects.Where(s => s.LecturerId == lecturerId)
            .Include(s => s.GroupSubjects)
            .ThenInclude(gs => gs.Group)
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
}