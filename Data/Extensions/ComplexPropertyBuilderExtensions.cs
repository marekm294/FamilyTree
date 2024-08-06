using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers.MaxLengthHelpers;
using Shared.Types;
using System.Linq.Expressions;

namespace Data.Extensions;

internal static class ComplexPropertyBuilderExtensions
{
    public static ComplexPropertyBuilder<TEntity> ComplexPropertyPlace<TEntity>(
        this ComplexPropertyBuilder<TEntity> builder,
        Expression<Func<TEntity, Place>> navigationExpression)
        where TEntity : class
    {
        builder.ComplexProperty(
            navigationExpression,
            complexPropertyBuilder =>
            {
                complexPropertyBuilder
                    .Property(p => p.Country)
                    .HasMaxLength(PlaceMaxLengthHelper.COUNTRY_MAX_LENGTH)
                    .IsRequired(false);

                complexPropertyBuilder
                    .Property(p => p.City)
                    .HasMaxLength(PlaceMaxLengthHelper.CITY_MAX_LENGTH)
                    .IsRequired(false);

                complexPropertyBuilder
                    .Property(p => p.Coordinates)
                    .HasColumnType("geometry")
                    .IsRequired(false);
            });

        return builder;
    }
}