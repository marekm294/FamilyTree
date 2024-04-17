using FluentValidation;
using Shared.Extensions;
using Shared.Helpers.MaxLengthHelpers;
using Shared.Models.Inputs.Families;

namespace Shared.Validators.Families;

internal sealed class UpdateFamilyInputValidator : AbstractValidator<UpdateFamilyInput>
{
    public UpdateFamilyInputValidator()
    {
        this
            .MustBeValidUpdateInput();

        RuleFor(f => f.FamilyName)
            .NotEmpty("Family Name")
            .MaximumLength(FamilyMaxLengthHelper.FAMILY_NAME_MAX_LENGTH, "Family Name");
    }
}