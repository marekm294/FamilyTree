using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.Extensions;

namespace Api.ModelBinders;

public sealed class Base64SafeQueryBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var formValues = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        var valueString = formValues.FirstValue;
        var value = Convert.FromBase64String(valueString?.ToBase64QueryUnsafe() ?? string.Empty);
        bindingContext.Result = ModelBindingResult.Success(value);

        return Task.CompletedTask;
    }
}