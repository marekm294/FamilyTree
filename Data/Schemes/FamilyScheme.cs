#pragma warning disable CS8618

using Data.Schemes.Abstraction;
using Domain.Entities;

namespace Data.Schemes;

internal class FamilyScheme : DbScheme, IFamily
{
    public string FamilyName { get; set; }
}