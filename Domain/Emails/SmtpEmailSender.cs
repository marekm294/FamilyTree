using Domain.Emails.Abstraction;
using Domain.Emails.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Domain.Emails;

internal sealed class SmtpEmailSender : ISmtpEmailSender
{
    private readonly SmtpOptions _smtpOptions;

    public SmtpEmailSender(IOptions<SmtpOptions> smtpOptions)
    {
        _smtpOptions = smtpOptions.Value;
    }

    public async Task<bool> SendEmailAsync(
        EmailMessage emailMessage,
        CancellationToken cancellationToken = default)
    {
        var wasEmailSent = true;
        using (var smtpClient = new SmtpClient())
        {
            try
            {
                await smtpClient.ConnectAsync(
                    _smtpOptions.Host,
                    _smtpOptions.Port,
                    cancellationToken: cancellationToken);

                if (_smtpOptions.HasConnectionAuthentication)
                {
                    await smtpClient.AuthenticateAsync(
                        _smtpOptions.Username,
                        _smtpOptions.Password,
                        cancellationToken);
                }

                using (var mimeMessage = GetMimeMessageFromEmailMessage(emailMessage))
                {
                    await smtpClient.SendAsync(mimeMessage);
                }
            }
            catch
            {
                wasEmailSent = false;
            }
            finally
            {
                await smtpClient.DisconnectAsync(true, cancellationToken);
            }

            return wasEmailSent;
        }
    }

    private static MimeMessage GetMimeMessageFromEmailMessage(EmailMessage emailMessage)
    {
        var mimeMessage = new MimeMessage();
        mimeMessage.From.AddRange(emailMessage.FromEmails.Select(em => new MailboxAddress(em.Name, em.Address)));
        mimeMessage.To.AddRange(emailMessage.ToEmails.Select(em => new MailboxAddress(em.Name, em.Address)));
        mimeMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = emailMessage.Body,
        };

        return mimeMessage;
    }
}
