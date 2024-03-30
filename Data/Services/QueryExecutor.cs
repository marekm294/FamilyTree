using Domain.DataServicesAbstraction;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

internal sealed class QueryExecutor : IQueryExecutor
{
    public Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default)
    {
        return query
            .ToListAsync(cancellationToken);
    }
}
