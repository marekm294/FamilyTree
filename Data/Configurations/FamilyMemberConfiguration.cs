using Data.Configurations.Abstraction;
using Data.Schemes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers.MaxLengthHelpers;

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
            .HasMaxLength(FamilyMemberMaxLenghtHelpers.FIRST_NAME_MAX_LENGTH);

        builder
            .Property(f => f.LastName)
            .IsRequired(true)
            .HasMaxLength(FamilyMemberMaxLenghtHelpers.LAST_NAME_MAX_LENGTH);

        builder
            .ToTable("FamilyMembers");
    }
}