using Shared.Models.Abstaction;
using Shared.Types;

namespace Shared.Models.Inputs.FamilyMembers;

public sealed class UpdateFamilyMemberInput : IUpdateInput
{
    public required Guid Id { get; init; }
    public required byte[] Version { get; init; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string[] MiddleNames { get; set; } = [];
    public Event Birth { get; set; } = new();
    public Event Death { get; set; } = new();
    public string? AboutMember { get; set; }
}