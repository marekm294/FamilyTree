using Data.ValueGenerators;
using Domain.Entities.Abstraction;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.Schemes;

namespace Data.Exceptions;

internal static class EntityTypeBuilderExtensions
{
    public static PropertyBuilder Tenant<TEntity>(this EntityTypeBuilder<TEntity> entityBuilder)
        where TEntity : class, ITenantable
    {
        entityBuilder
            .HasOne<TenantScheme>()
            .WithMany()
            .HasForeignKey(e => e.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        return entityBuilder
            .Property(e => e.TenantId)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<TenantValueGenerator>()
            .IsRequired();
    }
}