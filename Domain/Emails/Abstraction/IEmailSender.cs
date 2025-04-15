using Domain.Emails.Models;

namespace Domain.Emails.Abstraction;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(
        EmailMessage emailMessage,
        CancellationToken cancellationToken = default);
}
