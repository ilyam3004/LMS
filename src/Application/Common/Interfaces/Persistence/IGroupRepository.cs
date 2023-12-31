﻿using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IGroupRepository : IRepository<Group>
{
    Task<Group?> GetGroupByName(string name);
    Task<List<Group>> GetAllGroupsWithStudents();
    Task<Group?> GetGroupByIdWithStudents(Guid groupId);
}