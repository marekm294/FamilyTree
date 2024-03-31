using Domain.Exceptions.Abstraction;
using FluentValidation.Results;
using Shared.Models.Outputs;
using System.Net;

namespace Domain.Exceptions;

public class FamilyTreeValidationException : Exception, ICustomException
{
    private Dictionary<string, string[]> _failures = [];

    public FamilyTreeValidationException(string? message = null)
        : base(message ?? "One or more validation errors have occurred.")
    {
    }

    public FamilyTreeValidationException(ICollection<ValidationFailure> failures) : this()
    {
        _failures = failures
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(e => e.ErrorMessage).ToArray());
    }

    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public ErrorOutput ErrorOutput => new(Message, _failures);
}