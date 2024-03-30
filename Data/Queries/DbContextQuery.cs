using Data.Schemes.Abstraction;
using Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace Data.Queries;

internal sealed class DbContextQuery<TScheme, TEntity> : IQueryable<TEntity>, IAsyncEnumerable<TEntity>
    where TScheme : DbScheme, TEntity
    where TEntity : IEntity
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public DbContextQuery(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext ?? throw new ArgumentNullException(nameof(appDatabaseContext));
    }

    public Type ElementType => Query.ElementType;

    public Expression Expression => Query.Expression;

    public IQueryProvider Provider => Query.Provider;

    public IQueryable<TEntity> Query => CreateQuery();

    public IEnumerator<TEntity> GetEnumerator()
    {
        return Query.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return Query
            .AsAsyncEnumerable()
            .GetAsyncEnumerator();
    }

    public IQueryable<TEntity> CreateQuery()
    {
        return _appDatabaseContext
            .Set<TScheme>()
            .Cast<TEntity>();
    }
}