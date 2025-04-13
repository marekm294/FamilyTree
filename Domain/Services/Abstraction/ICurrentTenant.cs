namespace Domain.Services.Abstraction;

public interface ICurrentTenant
{
    public Guid TenantId { get; set; }
}