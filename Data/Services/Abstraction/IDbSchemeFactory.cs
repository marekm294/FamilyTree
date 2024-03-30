using Domain.Entities.Abstraction;

namespace Data.Services.Abstraction;

internal interface IDbSchemeFactory<TEntity>
    where TEntity : IEntity
{
    TEntity GetCreateEntity();
    TEntity GetUpdateEntity(Guid id, byte[] version);
}