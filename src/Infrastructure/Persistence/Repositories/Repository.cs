using Application.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly LmsDbContext DbContext;

    public Repository(LmsDbContext context)
    {
        DbContext = context;
    }

    public async Task AddAsync(TEntity entity)
        => await DbContext.Set<TEntity>().AddAsync(entity);

    public void Update(TEntity entity)
        => DbContext.Set<TEntity>().Update(entity);

    public async Task Remove(TEntity entity)
        => DbContext.Set<TEntity>().Remove(entity);

    public async Task<List<TEntity>> GetAll()
        => await DbContext.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(Guid id)
        => await DbContext.Set<TEntity>().FindAsync(id);
}