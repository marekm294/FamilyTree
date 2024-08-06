using FluentValidation;
using Shared.Types;

namespace Shared.Validators.TypeValidators;

internal class EventValidator : AbstractValidator<Event>
{
    public EventValidator(string placeName)
    {
        RuleFor(e => e.Place)
            .SetValidator(new PlaceValidator(placeName));
    }
}