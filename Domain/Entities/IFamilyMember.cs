using Domain.Entities.Abstraction;

namespace Domain.Entities;

public interface IFamilyMember : IEntity
{
    string FirstName { get; set; }

    string LastName { get; set; }

    DateTime? BirthDate { get; set; }

    DateTime? DeathDate { get; set; }

    public IFamilyMember InitializeFamilyMember(IFamilyMember familyMember)
    {
        familyMember.FirstName = FirstName;
        familyMember.LastName = LastName;
        familyMember.BirthDate = BirthDate;
        familyMember.DeathDate = DeathDate;

        return familyMember;
    }
}