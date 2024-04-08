#pragma warning disable CS8618

using Domain.Entities.Abstraction;

namespace Data.Schemes.Abstraction;

internal abstract class DbScheme : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public byte[] Version { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
}
