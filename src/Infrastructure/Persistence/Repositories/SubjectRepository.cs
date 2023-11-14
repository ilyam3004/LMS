using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class SubjectRepository(LmsDbContext context) : 
    Repository<Subject>(context), ISubjectRepository
{
    public Task<bool> ExistsByName(string name)
        => DbContext.Subjects.AnyAsync(s => s.Name == name);

    public Task<List<Subject>> GetLecturerSubjects(Guid lecturerId)
        => DbContext.Subjects.Where(s => s.LecturerId == lecturerId)
            .ToListAsync();
}
