using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Helpers.MaxLengthHelpers;
using Shared.Types;
using System.Linq.Expressions;

namespace Data.Extensions;

internal static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder ComplexPropertyEvent<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, Event>> navigationExpression)
        where TEntity : class
    {
        builder.ComplexProperty(
            navigationExpression,
            complexPropertyBuilder =>
            {
                complexPropertyBuilder
                    .Property(e => e.Date)
                    .IsRequired(false);
                complexPropertyBuilder
                    .Property(pn => pn.Place)
                    .HasMaxLength(EventMaxLengthHelper.EVENT_PLACE_MAX_LENGTH)
                    .IsRequired(false);
            });

        return builder;
    }
}