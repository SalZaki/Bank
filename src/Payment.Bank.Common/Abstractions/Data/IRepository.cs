using System.Linq.Expressions;
using Payment.Bank.Common.Abstractions.Domain;

namespace Payment.Bank.Common.Abstractions.Data;

public interface IRepository<TEntity, in TId> where TEntity : class, IEntity<TId>
{
    Task<TEntity> FindByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(IReadOnlyList<TEntity> entities, CancellationToken cancellationToken = default);
}
