using Domain.Entities.Abstraction;

namespace Domain.DataServicesAbstraction;

public interface IEntityProvider
{
    TEntity GetNewEntity<TEntity>()
        where TEntity : IEntity;

    TEntity GetExistingEntity<TEntity>(Guid id, byte[] version)
        where TEntity : IEntity;
}