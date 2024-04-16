#pragma warning disable CS8618

using Data.Schemes.Abstraction;
using Domain.Entities;

namespace Data.Schemes;

internal class FamilyMemberScheme : DbScheme, IFamilyMember
{
    public Guid FamilyId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string[] MiddleNames { get; set; } = [];

    public DateTime? BirthDate { get; set; }

    public DateTime? DeathDate { get; set; }
}