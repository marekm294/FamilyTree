using FluentValidation;
using Shared.Extensions;
using Shared.Helpers.MaxLengthHelpers;
using Shared.Models.Inputs.FamilyMembers;
using Shared.Types;
using Shared.Validators.TypeValidators;

namespace Shared.Validators.FamilyMembers;

internal sealed class CreateFamilyMemberInputValidator : AbstractValidator<CreateFamilyMemberInput>
{
    public CreateFamilyMemberInputValidator()
    {
        RuleFor(fm => fm.FamilyId)
            .NotEmpty("Family id");

        RuleFor(fm => fm.FirstName)
            .NotEmpty("First Name")
            .MaximumLength(FamilyMemberMaxLenghtHelpers.FIRST_NAME_MAX_LENGTH, "First Name");

        RuleFor(fm => fm.LastName)
            .NotEmpty("Last Name")
            .MaximumLength(FamilyMemberMaxLenghtHelpers.LAST_NAME_MAX_LENGTH, "Last Name");

        RuleFor(fm => fm.MiddleNames)
            .NotNull("Middle Names");

        RuleFor(fm => fm.Birth)
            .NotEmpty("Birth")
            .SetValidator(new EventValidator("BirthPlace"));

        RuleFor(fm => fm.Death)
            .NotEmpty("Death")
            .SetValidator(new EventValidator("DeathPlace"));
    }
}