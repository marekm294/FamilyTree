using Data.Configurations.Abstraction;
using Data.Extensions;
using Data.Schemes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

internal sealed class WeddingConfiguration : DbSchemeConfiguration<WeddingScheme>
{
    public override void Configure(EntityTypeBuilder<WeddingScheme> builder)
    {
        base
            .Configure(builder);

        builder
            .ComplexPropertyEvent(w => w.WeddingEvent);

        builder
            .HasOne<FamilyMemberScheme>()
            .WithMany()
            .HasForeignKey(w => w.PartnerId1)
            .HasPrincipalKey(fm => fm.Id)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        builder
            .HasOne<FamilyMemberScheme>()
            .WithMany()
            .HasForeignKey(w => w.PartnerId2)
            .HasPrincipalKey(fm => fm.Id)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        builder
            .ToTable("Weddings");
    }
}