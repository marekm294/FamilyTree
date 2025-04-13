using Domain.DataServicesAbstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.DomainServices;

internal sealed class QueryExecutor : IQueryExecutor
{
    public Task<List<TEntity>> ToListAsync<TEntity>(
        IQueryable<TEntity> query,
        CancellationToken cancellationToken = default)
    {
        return query
            .ToListAsync(cancellationToken);
    }

    public Task<T?> FirstOrDefaultAsyncAsync<T>(
        IQueryable<T> query,
        CancellationToken cancellationToken = default)
    {
        return query
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<T?> FirstOrDefaultAsyncAsync<T>(
        IQueryable<T> query,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return query
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}
