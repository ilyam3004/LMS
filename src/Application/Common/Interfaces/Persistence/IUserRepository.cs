using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<bool> UserExistsByEmail(string email);
    Task<bool> UserExistsById(Guid userId);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByIdWithRelations(Guid userId);
}