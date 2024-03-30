using Domain.Entities.Abstraction;

namespace Data.Entities.Abstraction;

internal abstract class DbEntity : IEntity
{
    public Guid Id { get; set; }
}
