using Shared.Models.Abstaction;

namespace Shared.Models.Outputs;

public sealed class FamilyOutput : IIdable
{
    public required Guid Id { get; init; }
    public required string FamilyName { get; init; }
    public required byte[] Version { get; init; }
}