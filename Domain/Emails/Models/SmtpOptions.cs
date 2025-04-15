namespace Domain.Emails.Models;

public sealed class SmtpOptions
{
    public const string SEECTION_MANE = "Emails:SmtpServer";

    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string? Username { get; set; } = null;
    public string? Password { get; set; } = null;

    public bool HasConnectionAuthentication => string.IsNullOrEmpty(Username) is false && string.IsNullOrEmpty(Password) is false;
}