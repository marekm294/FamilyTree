using Domain.Exceptions.Abstraction;
using Shared.Models.Outputs;
using System.Net;

namespace Data.Exceptions;

public sealed class EntityToUpdateNotFoundException : Exception, ICustomException
{
    public EntityToUpdateNotFoundException(Exception? innerException = null)
        : base("Updated entity was not found. Entity doesn't exist or was modified by different user. Reload data!", innerException)
    {

    }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public ErrorOutput ErrorOutput => new ErrorOutput(Message);
}
