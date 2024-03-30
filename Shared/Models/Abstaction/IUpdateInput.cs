namespace Shared.Models.Abstaction;

public interface IUpdateInput
{
    Guid Id { get; init; }
    byte[] Version { get; init; }
}