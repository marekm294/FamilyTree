using Email = (string Name, string Address);

namespace Domain.Emails.Models;

public sealed class EmailMessage
{
    public required Email[] FromEmails { get; init; }
    public required Email[] ToEmails { get; init; }
    public required string Body { get; init; }
}
