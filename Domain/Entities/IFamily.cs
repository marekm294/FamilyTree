using Domain.Entities.Abstraction;
using Shared.Models.Inputs.Families;

namespace Domain.Entities;

public interface IFamily : IEntity, ITenantable
{
    string FamilyName { get; set; }

    public void InitializeFamilyMember(CreateFamilyInput createFamilyInput)
    {
        FamilyName = createFamilyInput.FamilyName;
    }

    public void UpdateFamily(UpdateFamilyInput updateFamilyInput)
    {
        FamilyName = updateFamilyInput.FamilyName;
    }
}