using Praise.Domain.Entities;
using System.Linq.Expressions;

namespace Praise.Application.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);

    Task SaveChangesAsync();
}
