using System.Net;
using System.Net.Mail;

namespace Backend.Services;

public interface IEmailService
{
    Task<bool> SendEmail(string emailAddress, string subject, string body, bool isHtml = false);
}

public class EmailService(IConfiguration config) : IEmailService
{
    public async Task<bool> SendEmail(
        string emailAddress,
        string subject,
        string body,
        bool isHtml = false
    )
    {
        var to = new MailAddress(emailAddress);
        var from = new MailAddress(config.GetValue<string>("Email:Username") ?? "");
        var email = new MailMessage(from, to)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml,
        };

        using var smtp = new SmtpClient
        {
            Host = config.GetValue<string>("Email:Host") ?? "",
            Port = config.GetValue<int>("Email:Port"),
            Credentials = new NetworkCredential(
                config.GetValue<string>("Email:Username") ?? "",
                config.GetValue<string>("Email:Password") ?? ""
            ),
            EnableSsl = true,
        };

        try
        {
            await smtp.SendMailAsync(email);
            return true;
        }
        catch (SmtpException ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
}
