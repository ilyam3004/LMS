using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(LmsDbContext context) 
    : Repository<User>(context), IUserRepository 
{
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
            .Include(u => u.Student)
            .Include(u => u.Lecturer)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}