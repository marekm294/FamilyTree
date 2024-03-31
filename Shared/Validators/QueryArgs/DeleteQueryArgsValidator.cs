using FluentValidation;
using Shared.Extensions;
using Shared.QueryArgs;

namespace Shared.Validators.QueryArgs;

internal sealed class DeleteQueryArgsValidator : AbstractValidator<DeleteQueryArgs>
{
    public DeleteQueryArgsValidator()
    {
        RuleFor(d => d.Id)
            .NotEmpty("Id");

        RuleFor(c => c.Version)
            .NotEmpty("Version");
    }
}