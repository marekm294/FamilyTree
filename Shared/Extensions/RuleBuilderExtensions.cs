using FluentValidation;
using Shared.Models.Abstaction;

namespace Shared.Extensions;

internal static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, TProperty> NotEmpty<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder,
        string propertyName)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage($"{propertyName} is required");
    }

    public static IRuleBuilderOptions<T, string> MaximumLength<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int maximumLength,
        string propertyName)
    {
        return ruleBuilder
            .MaximumLength(maximumLength)
            .WithMessage($"Max length of {propertyName} is {maximumLength}");
    }

    public static void MustBeValidUpdateInput<T>(this AbstractValidator<T> abstractValidator)
        where T : class, IUpdateInput
    {
        abstractValidator
            .RuleFor(i => i.Id)
            .NotEmpty()
            .WithMessage("Id is required");

        abstractValidator
            .RuleFor(i => i.Version)
            .NotEmpty()
            .WithMessage("Version is required")
            .Must(v => v.Length == 8)
            .WithMessage("Must be valid version");
    }

    public static IRuleBuilderOptions<T, DateTime?> MustBeUtc<T>(
        this IRuleBuilder<T, DateTime?> ruleBuilder,
        string propertyName)
    {
        return ruleBuilder
            .Must(d => d is null || d.Value.Kind == DateTimeKind.Utc)
            .WithMessage($"{propertyName} must be UTC");
    }
}