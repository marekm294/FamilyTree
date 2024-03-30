namespace Domain.Entities.Abstraction;

public interface IEntity
{
    Guid Id { get; }
    byte[] Version { get; }
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
}