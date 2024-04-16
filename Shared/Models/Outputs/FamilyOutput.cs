namespace Shared.Models.Outputs;

public sealed class FamilyOutput
{
    public required Guid Id { get; init; }
    public required string FamilyName { get; init; }
    public required byte[] Version { get; init; }
}