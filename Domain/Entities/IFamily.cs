using Domain.Entities.Abstraction;
using Shared.Models.Inputs.Families;

namespace Domain.Entities;

public interface IFamily : IEntity
{
    string FamilyName { get; set; }

    public void InitializeFamilyMember(CreateFamilyInput createFamilyInput)
    {
        FamilyName = createFamilyInput.FamilyName;
    }
}