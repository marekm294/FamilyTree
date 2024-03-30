namespace Domain.DataServicesAbstraction;

public interface IQueryExecutor
{
    Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken = default);
}