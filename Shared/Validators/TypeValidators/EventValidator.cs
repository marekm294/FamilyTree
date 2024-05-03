﻿using FluentValidation;
using Shared.Extensions;
using Shared.Helpers.MaxLengthHelpers;
using Shared.Types;

namespace Shared.Validators.TypeValidators;

internal class EventValidator : AbstractValidator<Event>
{
    public EventValidator(string placeName)
    {
        RuleFor(e => e.Place)
            .MaximumLength(EventMaxLengthHelper.EVENT_PLACE_MAX_LENGTH, placeName);
    }
}