using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.Models.Outputs;

public sealed class ErrorOutput
{
    public ICollection<string> Errors { get; init; } = [];
    public IDictionary<string, string[]>? ValidationErrors { get; init; }

    [JsonConstructor]
    public ErrorOutput()
    {
    }

    public ErrorOutput(string message, IDictionary<string, string[]>? validationErrors = null)
    {
        Errors = [message];
        ValidationErrors = validationErrors?.ToDictionary();
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}