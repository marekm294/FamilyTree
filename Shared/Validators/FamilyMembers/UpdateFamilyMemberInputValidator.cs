using FluentValidation;
using Shared.Extensions;
using Shared.Helpers.MaxLengthHelpers;
using Shared.Models.Inputs.FamilyMembers;

namespace Shared.Validators.FamilyMembers;

internal sealed class UpdateFamilyMemberInputValidator : AbstractValidator<UpdateFamilyMemberInput>
{
    public UpdateFamilyMemberInputValidator()
    {
        this
            .MustBeValidUpdateInput();

        RuleFor(fm => fm.FirstName)
            .NotEmpty("First Name")
            .MaximumLength(FamilyMemberMaxLenghtHelpers.FIRST_NAME_MAX_LENGTH, "First Name");

        RuleFor(fm => fm.LastName)
            .NotEmpty("Last Name")
            .MaximumLength(FamilyMemberMaxLenghtHelpers.LAST_NAME_MAX_LENGTH, "Last Name");

        RuleFor(fm => fm.BirthDate)
            .MustBeUtc("Birth Date");

        RuleFor(fm => fm.DeathDate)
            .MustBeUtc("Death Date");
    }
}