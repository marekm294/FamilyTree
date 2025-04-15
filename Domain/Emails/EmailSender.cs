using Domain.Emails.Abstraction;
using Domain.Emails.Models;

namespace Domain.Emails;

internal sealed class EmailSender : IEmailSender
{
    private readonly ISmtpEmailSender _smtpEmailSender;

    public EmailSender(ISmtpEmailSender smtpEmailSender)
    {
        _smtpEmailSender = smtpEmailSender;
    }

    public Task<bool> SendEmailAsync(
        EmailMessage emailMessage,
        CancellationToken cancellationToken = default)
    {
        return _smtpEmailSender.SendEmailAsync(emailMessage);
    }
}
