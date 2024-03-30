using Data.Configurations.Abstraction;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

internal sealed class FamilyMemberConfiguration : DbEntityConfiguration<FamilyMemberEntity>
{
    public override void Configure(EntityTypeBuilder<FamilyMemberEntity> builder)
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