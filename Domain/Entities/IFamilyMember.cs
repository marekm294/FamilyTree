using Domain.Entities.Abstraction;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Types;

namespace Domain.Entities;

public interface IFamilyMember : IEntity
{
    Guid FamilyId {  get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string[] MiddleNames { get; set; }

    public Event Birth { get; set; }

    DateTime? DeathDate { get; set; }

    public void InitializeFamilyMember(CreateFamilyMemberInput createFamilyMemberInput)
    {
        FamilyId = createFamilyMemberInput.FamilyId;
        FirstName = createFamilyMemberInput.FirstName;
        LastName = createFamilyMemberInput.LastName;
        MiddleNames = createFamilyMemberInput.MiddleNames;
        Birth = createFamilyMemberInput.Birth;
        DeathDate = createFamilyMemberInput.DeathDate;
    }

    public void UpdateFamilyMember(UpdateFamilyMemberInput updateFamilyMemberInput)
    {
        FirstName = updateFamilyMemberInput.FirstName;
        LastName = updateFamilyMemberInput.LastName;
        MiddleNames = updateFamilyMemberInput.MiddleNames;
        Birth = updateFamilyMemberInput.Birth;
        DeathDate = updateFamilyMemberInput.DeathDate;
    }
}