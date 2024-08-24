using Data.Configurations.Abstraction;
using Data.Extensions;
using Data.Schemes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers.MaxLengthHelpers;

namespace Data.Configurations;

internal sealed class FamilyMemberConfiguration : DbSchemeConfiguration<FamilyMemberScheme>
{
    public override void Configure(EntityTypeBuilder<FamilyMemberScheme> builder)
    {
        base
            .Configure(builder);

        builder
            .Property(fm => fm.FirstName)
            .IsRequired(true)
            .HasMaxLength(FamilyMemberMaxLenghtHelpers.FIRST_NAME_MAX_LENGTH);

        builder
            .Property(fm => fm.LastName)
            .IsRequired(true)
            .HasMaxLength(FamilyMemberMaxLenghtHelpers.LAST_NAME_MAX_LENGTH);

        builder
            .PrimitiveCollection(fm => fm.MiddleNames);

        builder
            .ComplexPropertyEvent(fm => fm.Birth);

        builder
            .ComplexPropertyEvent(fm => fm.Death);

        builder
            .Property(fm => fm.AboutMember)
            .IsRequired(false);

        builder
            .ToTable("FamilyMembers");
    }
}