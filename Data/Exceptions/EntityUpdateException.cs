using Domain.Exceptions.Abstraction;
using Shared.Models.Outputs;
using System.Net;

namespace Data.Exceptions;

internal sealed class EntityUpdateException : Exception, ICustomException
{
    public EntityUpdateException(Exception? innerException = null)
        : base("Error while saving entity to database.", innerException)
    {
        
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public ErrorOutput ErrorOutput => new ErrorOutput(Message);
}
