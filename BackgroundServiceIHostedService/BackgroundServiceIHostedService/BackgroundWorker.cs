using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using MimeKit;

namespace BackgroundServiceIHostedService;

internal class BackgroundWorker : BackgroundService
{
    Timer timer;
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        timer = new Timer(SendReportByEmail, new MailData("asgar.babayev@code.edu.az","Report", "Hello RNET101")
            , TimeSpan.Zero, TimeSpan.FromSeconds(10));
        return Task.CompletedTask;
    }

    private void SendReportByEmail(object state)
    {
        Console.WriteLine("Email sending...");
        var email = new MimeMessage();
        MailData mailData = (MailData)state;

        email.Sender = MailboxAddress.Parse(EmailConfig.email);
        email.To.Add(MailboxAddress.Parse(mailData.To));
        email.Subject = mailData.Subject;
        var builder = new BodyBuilder();
        builder.HtmlBody = $"<h1 style='text-align:center;'>{mailData.Body}</h1>";
        email.Body = builder.ToMessageBody();
        using (SmtpClient smtp = new())
        {
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(EmailConfig.email, EmailConfig.password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
        Console.WriteLine("Email sent");
    }
}

//Shorthand Declaring
record MailData(string To, string Subject, string Body);

class EmailConfig
{
    public static string email = "asgerbabayev@hotmail.com";
    public static string password = "rarnto.com";
}
