﻿using Data.Entities.Abstraction;
using Data.ValueGenerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Abstraction;

internal abstract class DbEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : DbEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Version)
            .IsRowVersion();

        builder
            .Property(e => e.CreatedAt)
            .IsRequired(true)
            .ValueGeneratedOnAdd()
            .HasValueGenerator<DateTimeValueGenerator>();

        builder
            .Property(e => e.UpdatedAt)
            .IsRequired(false);
    }
}