#pragma warning disable CS8618

using Data.Entities.Abstraction;
using Domain.Entities;

namespace Data.Entities;

internal sealed class FamilyMemberEntity : DbEntity, IFamilyMember
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? DeathDate { get; set; }

    // TODO: add other names as json column string[]
}