namespace Shared.Models.Inputs.FamilyMembers;

public sealed class CreateFamilyMemberInput
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime? BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
}