using System.Linq.Expressions;

namespace Domain.DataServicesAbstraction;

public interface IQueryExecutor
{
    Task<List<T>> ToListAsync<T>(
        IQueryable<T> query,
        CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsyncAsync<T>(
        IQueryable<T> query,
        CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsyncAsync<T>(
        IQueryable<T> query,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);
}