using Microsoft.EntityFrameworkCore;
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
                    .ComplexPropertyPlace(pn => pn.Place)
                    .IsRequired(true);
            });

        return builder;
    }
}