using Data.Configurations.Abstraction;
using Data.Schemes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

internal sealed class FamilyMemberConfiguration : DbEntityConfiguration<FamilyMemberScheme>
{
    public override void Configure(EntityTypeBuilder<FamilyMemberScheme> builder)
    {
        base
            .Configure(builder);

        builder
            .Property(f => f.FirstName)
            .IsRequired(true)
            .HasMaxLength(32);

        builder
            .Property(f => f.LastName)
            .IsRequired(true)
            .HasMaxLength(32);

        builder
            .ToTable("FamilyMembers");
    }
}