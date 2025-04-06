using Data.Configurations.Abstraction;
using Data.Schemes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

internal sealed class TenantConfiguration : DbSchemeConfiguration<TenantScheme>
{
    public override void Configure(EntityTypeBuilder<TenantScheme> builder)
    {
        base
            .Configure(builder);

        builder
            .ToTable("Tenants");
    }
}
