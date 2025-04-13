using Domain.Entities.Abstraction;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Types;

namespace Domain.Entities;

public interface IFamilyMember : IEntity, ITenantable
{
    Guid FamilyId { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string[] MiddleNames { get; set; }

    Event Birth { get; set; }

    Event Death { get; set; }

    string? AboutMember { get; set; }

    public void InitializeFamilyMember(CreateFamilyMemberInput createFamilyMemberInput)
    {
        FamilyId = createFamilyMemberInput.FamilyId;
        FirstName = createFamilyMemberInput.FirstName;
        LastName = createFamilyMemberInput.LastName;
        MiddleNames = createFamilyMemberInput.MiddleNames;
        Birth = createFamilyMemberInput.Birth;
        Death = createFamilyMemberInput.Death;
        AboutMember = createFamilyMemberInput.AboutMember;
    }

    public void UpdateFamilyMember(UpdateFamilyMemberInput updateFamilyMemberInput)
    {
        FirstName = updateFamilyMemberInput.FirstName;
        LastName = updateFamilyMemberInput.LastName;
        MiddleNames = updateFamilyMemberInput.MiddleNames;
        Birth = updateFamilyMemberInput.Birth;
        Death = updateFamilyMemberInput.Death;
        AboutMember = updateFamilyMemberInput.AboutMember;
    }
}