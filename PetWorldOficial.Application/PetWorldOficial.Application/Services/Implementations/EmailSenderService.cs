using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Configuration.SendInBlue;
using PetWorldOficial.Application.Services.Interfaces;

namespace PetWorldOficial.Application.Services.Implementations;

public class EmailSenderService(
    IOptions<SendInBlueConfiguration> options) : IEmailSenderService
{
    public async Task SendAsync(string to, string subject, string body)
    {
        var client = new SmtpClient(options.Value.SMTP, options.Value.Port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(options.Value.From, options.Value.SecretKey),
            UseDefaultCredentials = false
        };
        
        var mailMessage = new MailMessage
        {
            From = new MailAddress(options.Value.From),
            To = { to },
            Subject = subject,
            Body = body,
            IsBodyHtml = false
        };

        try
        {
            await client.SendMailAsync(mailMessage);
        }
        catch (Exception)
        {
            throw;
        }
    }
}