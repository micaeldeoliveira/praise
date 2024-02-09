using Microsoft.EntityFrameworkCore;
using Praise.Application.Interfaces.Repositories;
using Praise.Domain.Entities;
using Praise.Infra.Contexts;
using System.Linq.Expressions;

namespace Praise.Infra.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly AppDbContext _context;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(AppDbContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
        await DbSet.AsNoTracking().Where(predicate).ToListAsync();

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) =>
        await DbSet.AnyAsync(predicate);

    public virtual async Task<TEntity> GetByIdAsync(Guid id) => await DbSet.FindAsync(id);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await DbSet.ToListAsync();

    public virtual async Task AddAsync(TEntity entity) => await DbSet.AddAsync(entity);

    public virtual void Update(TEntity entity) => DbSet.Update(entity);

    public virtual void Remove(TEntity entity) => DbSet.Remove(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context?.Dispose();
}