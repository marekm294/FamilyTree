using Shared.Models.Abstaction;

namespace Shared.Models.Inputs.Families;

public sealed class UpdateFamilyInput : IUpdateInput
{
    public required Guid Id { get; init; }
    public required byte[] Version { get; init; }
    public required string FamilyName { get; set; }
}