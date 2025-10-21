using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
namespace Identity.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

            using var smtp = new SmtpClient();
            var host = _config["EmailSettings:SmtpServer"];          // sandbox.smtp.mailtrap.io
            var port = int.Parse(_config["EmailSettings:Port"]);     // 587
            var user = _config["EmailSettings:Username"];
            var pass = _config["EmailSettings:Password"];

            // 🔧 ÖNEMLİ: 587 için StartTLS kullan
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(user, pass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            _logger.LogInformation("Email sent to {To}", toEmail);
        }
    }
}
