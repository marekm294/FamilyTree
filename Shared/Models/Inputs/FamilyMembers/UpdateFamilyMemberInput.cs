using Shared.Models.Abstaction;

namespace Shared.Models.Inputs.FamilyMembers;

public sealed class UpdateFamilyMemberInput : IUpdateInput
{
    public required Guid Id { get; init; }
    public required byte[] Version { get; init; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime? BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
}