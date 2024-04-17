using Data.Services.Abstraction;
using Domain.DataServicesAbstraction;
using Domain.Entities.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models.Abstaction;

namespace Data.DomainServices;

internal sealed class EntityProvider : IEntityProvider
{
    private readonly IServiceProvider _serviceProvider;

    public EntityProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public TEntity GetNewEntity<TEntity>()
        where TEntity : IEntity
    {
        var dbSchemeFactory = _serviceProvider.GetRequiredService<IDbSchemeFactory<TEntity>>();
        return dbSchemeFactory.GetCreateEntity();
    }

    public TEntity GetExistingEntity<TEntity, TUpdateInput>(TUpdateInput updateInput)
        where TEntity : IEntity
        where TUpdateInput : class, IUpdateInput
    {
        return GetExistingEntity<TEntity>(updateInput.Id, updateInput.Version);
    }

    public TEntity GetExistingEntity<TEntity>(Guid id, byte[] version)
        where TEntity : IEntity
    {
        var dbSchemeFactory = _serviceProvider.GetRequiredService<IDbSchemeFactory<TEntity>>();
        return dbSchemeFactory.GetUpdateEntity(id, version);
    }
}