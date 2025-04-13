using Data.Configurations.Abstraction;
using Data.Exceptions;
using Data.Schemes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers.MaxLengthHelpers;

namespace Data.Configurations;

internal sealed class FamilyConfiguration : DbSchemeConfiguration<FamilyScheme>
{
    public override void Configure(EntityTypeBuilder<FamilyScheme> builder)
    {
        base
            .Configure(builder);

        builder
            .Property(f => f.FamilyName)
            .IsRequired(true)
            .HasMaxLength(FamilyMaxLengthHelper.FAMILY_NAME_MAX_LENGTH);

        builder
            .HasMany<FamilyMemberScheme>()
            .WithOne()
            .HasForeignKey(fm => fm.FamilyId)
            .HasPrincipalKey(f => f.Id);

        builder
            .Tenant();

        builder
            .ToTable("Families");
    }
}