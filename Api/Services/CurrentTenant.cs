using Domain.Services.Abstraction;

namespace Api.Services;

public sealed class CurrentTenant : ICurrentTenant
{
    public Guid TenantId { get; set; }
}