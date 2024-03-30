using Domain.DataServicesAbstraction;
using Domain.Extensions;
using Domain.Services.Abstraction;

namespace Domain.Services;

internal sealed class DomainConfigurationService : IDomainConfigurationService
{
    private readonly IQueryExecutor _queryExecutor;

    public DomainConfigurationService(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
    }

    public void ConfigureDomain()
    {
        QueryExtensions.Configure(_queryExecutor);
    }
}
