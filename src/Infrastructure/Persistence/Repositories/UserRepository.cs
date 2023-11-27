using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(LmsDbContext context) : base(context)
    {
    }

    public async Task<bool> UserExistsByEmail(string email)
        => await DbContext.Users
            .AnyAsync(u => u.Email == email);

    public async Task<bool> UserExistsById(Guid userId)
        => await DbContext.Users
            .AnyAsync(u => u.UserId == userId);

    public async Task<User?> GetUserByEmail(string email)
        => await DbContext.Users
            .Include(u => u.Student)
            .Include(u => u.Lecturer)
            .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetUserByIdWithRelations(Guid userId)
        => await DbContext.Users.Include(u => u.Student)
            .ThenInclude(s => s.Group)
            .Include(u => u.Lecturer)
            .FirstOrDefaultAsync(u => u.UserId == userId);
}