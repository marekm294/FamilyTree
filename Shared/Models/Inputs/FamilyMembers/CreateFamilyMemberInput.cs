using Shared.Types;

namespace Shared.Models.Inputs.FamilyMembers;

public sealed class CreateFamilyMemberInput
{
    public Guid FamilyId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string[] MiddleNames { get; set; } = [];
    public Event Birth { get; set; } = new();
    public Event Death { get; set; } = new();
}