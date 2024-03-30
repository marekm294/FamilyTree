using Services.Extensions;
using Shared.QueryArgs.Abstraction;

namespace Shared.QueryArgs;

public sealed record DeleteQueryArgs(Guid Id, byte[] Version) : IQueryArgs
{
    public string ToQueryString()
    {
        return $"{nameof(Id)}={Id}&{nameof(Version)}={Convert.ToBase64String(Version).ToBase64QuerySafe()}";
    }
}