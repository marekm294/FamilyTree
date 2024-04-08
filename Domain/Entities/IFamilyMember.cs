using Domain.Entities.Abstraction;
using Shared.Models.Inputs.FamilyMembers;

namespace Domain.Entities;

public interface IFamilyMember : IEntity
{
    string FirstName { get; set; }

    string LastName { get; set; }

    string[] MiddleNames { get; set; }

    DateTime? BirthDate { get; set; }

    DateTime? DeathDate { get; set; }

    public void InitializeFamilyMember(CreateFamilyMemberInput createFamilyMemberInput)
    {
        FirstName = createFamilyMemberInput.FirstName;
        LastName = createFamilyMemberInput.LastName;
        MiddleNames = createFamilyMemberInput.MiddleNames;
        BirthDate = createFamilyMemberInput.BirthDate;
        DeathDate = createFamilyMemberInput.DeathDate;
    }

    public void UpdateFamilyMember(UpdateFamilyMemberInput updateFamilyMemberInput)
    {
        FirstName = updateFamilyMemberInput.FirstName;
        LastName = updateFamilyMemberInput.LastName;
        MiddleNames = updateFamilyMemberInput.MiddleNames;
        BirthDate = updateFamilyMemberInput.BirthDate;
        DeathDate = updateFamilyMemberInput.DeathDate;
    }
}