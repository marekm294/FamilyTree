using Domain.Entities.Abstraction;

namespace Domain.DataServicesAbstraction;

public interface IEntityProvider
{
    TEntity GetCreateEntity<TEntity>()
        where TEntity : IEntity;

    TEntity GetUpdateEntity<TEntity>(Guid id, byte[] version)
        where TEntity : IEntity;
}