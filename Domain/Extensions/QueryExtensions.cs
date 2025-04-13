using Domain.DataServicesAbstraction;
using System.Linq.Expressions;

namespace Domain.Extensions;

internal static class QueryExtensions
{
    private static IQueryExecutor _queryExecutor = null!;

    public static void Configure(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public static Task<List<T>> ToListAsync<T>(
        this IQueryable<T> query,
        CancellationToken cancellationToken = default)
    {
        return _queryExecutor.ToListAsync(query, cancellationToken);
    }

    public static Task<T?> FirstOrDefaultAsync<T>(
        this IQueryable<T> query,
        CancellationToken cancellationToken = default)
    {
        return _queryExecutor.FirstOrDefaultAsyncAsync(query, cancellationToken);
    }

    public static Task<T?> FirstOrDefaultAsync<T>(
        this IQueryable<T> query,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return _queryExecutor.FirstOrDefaultAsyncAsync(query, predicate, cancellationToken);
    }
}