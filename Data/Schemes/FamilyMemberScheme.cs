#pragma warning disable CS8618

using Data.Schemes.Abstraction;
using Domain.Entities;
using Shared.Types;

namespace Data.Schemes;

internal class FamilyMemberScheme : DbScheme, IFamilyMember
{
    public Guid FamilyId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string[] MiddleNames { get; set; } = [];

    public Event Birth { get; set; } = new();

    public DateTime? DeathDate { get; set; }
}