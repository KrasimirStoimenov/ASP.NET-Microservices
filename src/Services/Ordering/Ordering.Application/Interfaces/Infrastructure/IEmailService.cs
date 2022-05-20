namespace Ordering.Application.Interfaces.Infrastructure;

using Ordering.Application.Models;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Email email);
}
