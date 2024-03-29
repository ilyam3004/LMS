﻿using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class GroupRepository : Repository<Group>, IGroupRepository
{
    public GroupRepository(LmsDbContext context) : base(context)
    { }

    public async Task<Group?> GetGroupByName(string name)
        => await DbContext.Groups.Include(g => g.Subjects)
            .ThenInclude(s => s.Tasks)
            .FirstOrDefaultAsync(g => g.Name == name);

    public async Task<List<Group>> GetAllGroupsWithStudents()
        => await DbContext.Groups.Include(g => g.Students)
            .ToListAsync();

    public async Task<Group?> GetGroupByIdWithStudents(Guid groupId)
        => await DbContext.Groups.Include(g => g.Students)
            .FirstOrDefaultAsync(g => g.GroupId == groupId);
}