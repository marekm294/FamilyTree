using Domain.Entities.Abstraction;
using Shared.Models.Inputs.FamilyMembers;

namespace Domain.Entities;

public interface IFamilyMember : IEntity
{
    string FirstName { get; set; }

    string LastName { get; set; }

    DateTime? BirthDate { get; set; }

    DateTime? DeathDate { get; set; }

    public void InitializeFamilyMember(CreateFamilyMemberInput createFamilyMemberInput)
    {
        FirstName = createFamilyMemberInput.FirstName;
        LastName = createFamilyMemberInput.LastName;
        BirthDate = createFamilyMemberInput.BirthDate;
        DeathDate = createFamilyMemberInput.DeathDate;
    }

    public void UpdateFamilyMember(UpdateFamilyMemberInput updateFamilyMemberInput)
    {
        FirstName = updateFamilyMemberInput.FirstName;
        LastName = updateFamilyMemberInput.LastName;
        BirthDate = updateFamilyMemberInput.BirthDate;
        DeathDate = updateFamilyMemberInput.DeathDate;
    }
}