using Domain.Exceptions.Abstraction;
using Shared.Models.Outputs;
using System.Net;

namespace Api.Exceptions;

public sealed class TenantDeactivatedException : Exception, ICustomException
{
    public TenantDeactivatedException()
        : base("Tenant was deactivated.")
    {
    }

    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public ErrorOutput ErrorOutput => new ErrorOutput(Message);
}
