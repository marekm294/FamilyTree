#pragma warning disable CS8618

using Data.Schemes.Abstraction;
using Domain.Entities;

namespace Data.Schemes;

internal class TenantScheme : DbScheme, ITenant
{
    public bool IsActive { get; set; }
}
