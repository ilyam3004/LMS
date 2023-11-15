using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class SubjectRepository(LmsDbContext context) : 
    Repository<Subject>(context), ISubjectRepository
{
    public Task<List<Subject>> GetLecturerSubjects(Guid lecturerId)
        => DbContext.Subjects.Where(s => s.LecturerId == lecturerId)
            .ToListAsync();
}
