using Data.Schemes.Abstraction;
using Domain.Entities;
using Shared.Types;

namespace Data.Schemes;

internal class WeddingScheme : DbScheme, IWedding
{
    public Guid? PartnerId1 { get; set; }
    public Guid? PartnerId2 { get; set; }
    public Event WeddingEvent { get; set; } = new();
    public Guid TenantId { get; set; }
}