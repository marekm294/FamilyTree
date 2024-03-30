using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

internal sealed class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMemberEntity>
{
    public void Configure(EntityTypeBuilder<FamilyMemberEntity> builder)
    {
        builder
            .HasKey(f => f.Id);

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