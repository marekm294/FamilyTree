using Api.Exceptions;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.FilterAttributes;

public sealed class ValidationFilter<TInput> : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var input = context
            .ActionArguments
            .Select(a => a.Value)
            .OfType<TInput>()
            .FirstOrDefault();

        if (input is null)
        {
            var exception = new TypeBindingException(typeof(TInput).Name);
            context.Result = new BadRequestObjectResult(exception.ErrorOutput);
            return;
        }

        var serviceProvider = context.HttpContext.RequestServices;
        var validator = serviceProvider.GetRequiredService<IValidator<TInput>>();
        var validationContext = new ValidationContext<TInput>(input);

        var validationResult = validator.Validate(validationContext!);

        var failures = validationResult.Errors;
        if (failures.Count != 0)
        {
            var exception = new FamilyTreeValidationException(failures);
            context.Result = new BadRequestObjectResult(exception.ErrorOutput);
        }
    }
}