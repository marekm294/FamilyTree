using Domain.Entities;

namespace Domain.DataServices.Abstraction;

public interface ITenantService : IDataService
{
    Task<ITenant?> GetTenantByIdAsync(
        Guid tenantId,
        CancellationToken cancellationToken = default);
}
