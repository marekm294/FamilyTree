using Domain.Exceptions.Abstraction;
using Shared.Models.Outputs;
using System.Net;

namespace Api.Exceptions;

public sealed class InvalidTenantIdException : Exception, ICustomException
{
    public InvalidTenantIdException()
        : base("Invalid Tenant!")
    {
    }

    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;
    public ErrorOutput ErrorOutput => new ErrorOutput(Message);
}
