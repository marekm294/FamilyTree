using Domain.DataServicesAbstraction;
using Domain.Entities.Abstraction;

namespace Data.DomainServices;

internal sealed class DbOperation : IDbOperation
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public DbOperation(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext ?? throw new ArgumentNullException(nameof(appDatabaseContext));
    }

    public async Task<TEntity> AddAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : IEntity
    {
        await _appDatabaseContext.AddAsync(entity, cancellationToken);
        return entity;
    }

    public void AllowUpdate<TEntity>(TEntity entity)
        where TEntity : IEntity
    {
        _appDatabaseContext.Attach(entity); 
    }

    public void Remove<TEntity>(TEntity entity)
        where TEntity : IEntity
    {
        _appDatabaseContext.Remove(entity);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _appDatabaseContext.SaveChangesAsync(cancellationToken);
    }
}