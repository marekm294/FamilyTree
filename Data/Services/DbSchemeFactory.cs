using Data.Schemes.Abstraction;
using Data.Services.Abstraction;
using Domain.Entities.Abstraction;

namespace Data.Services;

internal sealed class DbSchemeFactory<TScheme, TEntity> : IDbSchemeFactory<TEntity>
    where TScheme : DbScheme, TEntity, new()
    where TEntity : IEntity
{
    public TEntity GetCreateEntity()
    {
        return new TScheme();
    }

    public TEntity GetUpdateEntity(Guid id, byte[] version)
    {
        return new TScheme()
        {
            Id = id,
            Version = version,
        };
    }
}