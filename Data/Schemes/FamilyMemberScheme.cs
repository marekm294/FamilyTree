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
    public Event Death { get; set; } = new();
    public string? AboutMember { get; set; }
    public Guid TenantId { get; set; }
}