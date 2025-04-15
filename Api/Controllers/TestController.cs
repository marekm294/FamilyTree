using Api.Controllers.Abstraction;
using Domain.Emails.Abstraction;
using Domain.Emails.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestController : BaseController
{
    // This is just endpoint to test emails
    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmailAsync(
        [FromServices] IEmailSender emailSender,
        [FromQuery] string message = "WelcomeMail",
        [FromQuery] string fromEmail = "familyTree@gmail.com",
        [FromQuery] string toEmail = "randomEmail@gmail.com")
    {
        var emailMessage = new EmailMessage()
        {
            Body = message,
            FromEmails = [("Family Tree", fromEmail)],
            ToEmails = [("Name of receiver", toEmail)],
        };
        
        var wasEmailSent = await emailSender.SendEmailAsync(emailMessage);
        
        if (wasEmailSent)
        {
            return Ok("Email was sent!");
        }

        return BadRequest("Email was NOT sent!");
    }
}
