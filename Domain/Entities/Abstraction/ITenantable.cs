namespace Domain.Entities.Abstraction;

public interface ITenantable
{
    public Guid TenantId { get; set; }
}