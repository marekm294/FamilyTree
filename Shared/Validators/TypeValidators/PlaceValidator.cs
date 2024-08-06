using FluentValidation;
using Shared.Extensions;
using Shared.Helpers.MaxLengthHelpers;
using Shared.Types;

namespace Shared.Validators.TypeValidators;

internal class PlaceValidator : AbstractValidator<Place>
{
    public PlaceValidator(string placeName)
    {
        RuleFor(p => p.Country)
           .MaximumLength(PlaceMaxLengthHelper.COUNTRY_MAX_LENGTH, $"{placeName}.Country");

        RuleFor(p => p.City)
            .MaximumLength(PlaceMaxLengthHelper.CITY_MAX_LENGTH, $"{placeName}.Country");
    }
}