using Domain.Entities.Abstraction;

namespace Domain.Entities;

public interface ITenant : IEntity
{
    public bool IsActive { get; set; }
}
