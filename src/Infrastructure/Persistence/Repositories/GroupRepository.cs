using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GroupRepository(LmsDbContext context) : 
    Repository<Group>(context), IGroupRepository
{
    public async Task<Group?> GetGroupByName(string name)
        => await DbContext.Groups.FirstOrDefaultAsync(g =>
            g.Name == name);
}