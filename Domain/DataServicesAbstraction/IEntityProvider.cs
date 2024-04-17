using Domain.Entities.Abstraction;
using Shared.Models.Abstaction;

namespace Domain.DataServicesAbstraction;

public interface IEntityProvider
{
    TEntity GetNewEntity<TEntity>()
        where TEntity : IEntity;

    public TEntity GetExistingEntity<TEntity, TUpdateInput>(TUpdateInput updateInput)
        where TEntity : IEntity
        where TUpdateInput : class, IUpdateInput;

    public TEntity GetExistingEntity<TEntity>(
        Guid id,
        byte[] version)
        where TEntity : IEntity;
}