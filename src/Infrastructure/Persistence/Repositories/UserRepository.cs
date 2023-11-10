﻿using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository 
{
    public UserRepository(LmsDbContext context) : base(context)
    {
        
    }
    public async Task<bool> UserExistsByEmail(string email)
    {
        return await DbContext.Users
            .AnyAsync(u => u.Email == email);
    }

    public async Task<bool> UserExistsById(Guid userId)
    {
        return await DbContext.Users
            .AnyAsync(u => u.UserId == userId);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await DbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}