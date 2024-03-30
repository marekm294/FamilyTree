using Domain.Entities.Abstraction;

namespace Data.Entities.Abstraction;

internal abstract class DbEntity : IEntity
{
    public Guid Id { get; set; }
    public byte[] Version { get; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
}
