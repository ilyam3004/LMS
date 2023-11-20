using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GroupRepository(LmsDbContext context) :
    Repository<Group>(context), IGroupRepository
{
    public async Task<Group?> GetGroupByName(string name)
        => await DbContext.Groups.Include(g => g.GroupSubjects)
            .ThenInclude(gs => gs.Subject)
            .FirstOrDefaultAsync(g => g.Name == name);

    public async Task<List<Group>> GetAllGroupsWithStudents()
        => await DbContext.Groups.
            Include(g => g.Students)
            .ToListAsync();
}