using Api.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.ModelBinderProviders;

public sealed class ModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(byte[]))
        {
            return new Base64SafeQueryBinder();
        }

        return null;
    }
}