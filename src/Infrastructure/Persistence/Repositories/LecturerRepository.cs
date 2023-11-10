using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class LecturerRepository : Repository<Lecturer>, ILecturerRepository
{
    public LecturerRepository(LmsDbContext context) : base(context)
    { }
}