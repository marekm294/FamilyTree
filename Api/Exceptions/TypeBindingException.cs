using Domain.Exceptions.Abstraction;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Models.Outputs;
using System.Net;

namespace Api.Exceptions;

public sealed class TypeBindingException : Exception, ICustomException
{
    public TypeBindingException(string propertyType)
        : base($"{propertyType} is required.")
    {
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public ErrorOutput ErrorOutput => new(Message);
}