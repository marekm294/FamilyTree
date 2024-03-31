using Shared.Models.Outputs;
using System.Net;

namespace Domain.Exceptions.Abstraction;

public interface ICustomException
{
    public HttpStatusCode StatusCode { get; }

    public ErrorOutput ErrorOutput { get; }
}