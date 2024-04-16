using FluentValidation;
using Shared.Extensions;
using Shared.Models.Inputs.Families;

namespace Shared.Validators.Families;

internal sealed class CreateFamilyInputValidator : AbstractValidator<CreateFamilyInput>
{
    public CreateFamilyInputValidator()
    {
        RuleFor(f => f.FamilyName)
            .NotEmpty("Family name");
    }
}