using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Outputs;

namespace Api.FilterAttributes;

public sealed class RequiredGuidFilter(string parameterName) : ActionFilterAttribute
{
    private readonly string _parameterName = parameterName;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var actionArgument = context.ActionArguments.FirstOrDefault(a => a.Key == _parameterName);
        if (actionArgument.Value is not Guid id || id == Guid.Empty)
        {
            context.Result = new BadRequestObjectResult(new ErrorOutput($"{_parameterName} is required"));
        }

        base.OnActionExecuting(context);
    }
}