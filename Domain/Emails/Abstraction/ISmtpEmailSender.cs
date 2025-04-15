using Domain.Emails.Models;

namespace Domain.Emails.Abstraction;

internal interface ISmtpEmailSender
{
    Task<bool> SendEmailAsync(
        EmailMessage emailMessage,
        CancellationToken cancellationToken = default);
}