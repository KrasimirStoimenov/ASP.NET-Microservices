namespace Ordering.Infrastructure.Mail;

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Ordering.Application.Interfaces.Infrastructure;
using Ordering.Application.Models;

using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService : IEmailService
{
    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        this.emailSettings = emailSettings.Value;
        this.logger = logger;
    }

    public EmailSettings emailSettings { get; }

    public ILogger<EmailService> logger { get; }

    public async Task<bool> SendEmailAsync(Email email)
    {
        //SendGrid implementation
        var client = new SendGridClient(this.emailSettings.ApiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = this.emailSettings.FromAddress,
            Name = this.emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        this.logger.LogInformation("Email sent.");

        if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }

        this.logger.LogError("Email sending failed.");

        return false;
    }
}
