using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Outputs;

namespace Api.FilterAttributes;

public sealed class RequiredFilter(string parameterName) : ActionFilterAttribute
{
    private readonly string _parameterName = parameterName;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var isActionArgumentInContext = context.ActionArguments.Any(a => a.Key == _parameterName);
        if (isActionArgumentInContext is false)
        {
            context.Result = new BadRequestObjectResult(new ErrorOutput($"{_parameterName} is required"));
        }

        base.OnActionExecuting(context);
    }
}