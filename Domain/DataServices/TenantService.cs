using Domain.DataServices.Abstraction;
using Domain.Entities;
using Domain.Extensions;

namespace Domain.DataServices;

internal sealed class TenantService : ITenantService
{
    private readonly IQueryable<ITenant> _tenants;

    public TenantService(IQueryable<ITenant> tenants)
    {
        _tenants = tenants;
    }

    public async Task<ITenant?> GetTenantByIdAsync(
        Guid tenantId,
        CancellationToken cancellationToken = default)
    {
        return await _tenants
            .FirstOrDefaultAsync(t => t.Id == tenantId, cancellationToken);
    }
}
