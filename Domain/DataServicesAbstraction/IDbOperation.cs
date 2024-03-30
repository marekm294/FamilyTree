using Domain.Entities.Abstraction;

namespace Domain.DataServicesAbstraction;

public interface IDbOperation
{
    Task<TEntity> AddAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : IEntity;

    void AllowUpdate<TEntity>(TEntity entity)
        where TEntity : IEntity;

    void Remove<TEntity>(TEntity entity)
        where TEntity : IEntity;

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}