using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class StudentTaskRepository : Repository<StudentTask>, 
    IStudentTaskRepository
{
    public StudentTaskRepository(LmsDbContext context) : base(context)
    {
    }
}