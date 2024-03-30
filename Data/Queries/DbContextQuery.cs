using Data.Entities.Abstraction;
using Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace Data.Queries;

internal sealed class DbContextQuery<TEntity, TResult> : IQueryable<TResult>, IAsyncEnumerable<TResult>
    where TEntity : DbEntity, TResult
    where TResult : IEntity
{
    private readonly AppDatabaseContext _appDatabaseContext;

    public DbContextQuery(AppDatabaseContext appDatabaseContext)
    {
        _appDatabaseContext = appDatabaseContext ?? throw new ArgumentNullException(nameof(appDatabaseContext));
    }

    public Type ElementType => Query.ElementType;

    public Expression Expression => Query.Expression;

    public IQueryProvider Provider => Query.Provider;

    protected IQueryable<TResult> Query => CreateQuery();

    public IEnumerator<TResult> GetEnumerator()
    {
        return Query.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Query).GetEnumerator();
    }

    public IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return Query
            .AsAsyncEnumerable()
            .GetAsyncEnumerator();
    }

    public IQueryable<TResult> CreateQuery()
    {
        return _appDatabaseContext
            .Set<TEntity>()
            .Cast<TResult>();
    }
}