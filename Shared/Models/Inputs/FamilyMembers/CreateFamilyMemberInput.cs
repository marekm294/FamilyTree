namespace Shared.Models.Inputs.FamilyMembers;

public sealed class CreateFamilyMemberInput
{
    public Guid FamilyId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string[] MiddleNames { get; set; } = [];
    public DateTime? BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
}