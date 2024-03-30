using Domain.DataServicesAbstraction;
using Microsoft.EntityFrameworkCore;

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
}
