using AkademiQMongoDb.DTOs.MailDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Services.EmailServices;
using AkademiQMongoDb.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace AkademiQMongoDb.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(EmailDto emailDto)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            email.To.Add(new MailboxAddress("", emailDto.To));
            email.Subject = emailDto.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendBulkEmailAsync(List<string> recipients, string subject, string body)
        {
            var tasks = new List<Task>();

            foreach (var recipient in recipients)
            {
                var emailDto = new EmailDto
                {
                    To = recipient,
                    Subject = subject,
                    Body = body
                };

                tasks.Add(SendEmailAsync(emailDto));

                // Rate limiting - çok fazla mail atmamak için
                if (tasks.Count >= 10)
                {
                    await Task.WhenAll(tasks);
                    tasks.Clear();
                    await Task.Delay(1000); // 1 saniye bekle
                }
            }

            if (tasks.Any())
            {
                await Task.WhenAll(tasks);
            }
        }

        public async Task SendDiscountEmailToSubscribersAsync(string discountCode, int discountPercentage)
        {
            // HTML şablonu oluştur
            var htmlBody = GetDiscountEmailTemplate(discountCode, discountPercentage);

            // Aboneleri getir (bunu SubscriberService'den alacaksın)
            var subscribers = await GetActiveSubscribersAsync(); // Bu metodu sonra oluşturacağız

            var recipientEmails = subscribers.Select(s => s.Email).ToList();

            await SendBulkEmailAsync(
                recipientEmails,
                $"🎉 {discountPercentage}% İndirim Kazandınız!",
                htmlBody
            );
        }

        private string GetDiscountEmailTemplate(string discountCode, int discountPercentage)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <style>
                    body {{
                        font-family: 'Jost', Arial, sans-serif;
                        background-color: #f5f5f5;
                        margin: 0;
                        padding: 0;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background-color: #ffffff;
                        border-radius: 10px;
                        overflow: hidden;
                        box-shadow: 0 5px 20px rgba(0,0,0,0.1);
                    }}
                    .header {{
                        background-color: #EB0029;
                        color: white;
                        padding: 30px;
                        text-align: center;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 32px;
                        font-family: 'Oswald', sans-serif;
                    }}
                    .content {{
                        padding: 40px 30px;
                        text-align: center;
                    }}
                    .discount-code {{
                        background-color: #f8f8f8;
                        border: 2px dashed #EB0029;
                        border-radius: 10px;
                        padding: 20px;
                        margin: 25px 0;
                        font-size: 32px;
                        font-weight: bold;
                        color: #EB0029;
                        letter-spacing: 3px;
                    }}
                    .button {{
                        display: inline-block;
                        background-color: #EB0029;
                        color: white;
                        text-decoration: none;
                        padding: 15px 40px;
                        border-radius: 8px;
                        font-weight: 600;
                        margin-top: 20px;
                        font-family: 'Oswald', sans-serif;
                    }}
                    .footer {{
                        background-color: #f8f8f8;
                        padding: 20px;
                        text-align: center;
                        font-size: 12px;
                        color: #666;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Foodu Restaurant</h1>
                    </div>
                    <div class='content'>
                        <h2 style='color: #072b31;'>Özel İndirim Fırsatı!</h2>
                        <p style='font-size: 18px; margin-bottom: 20px;'>
                            Size özel <strong style='color: #EB0029;'>{discountPercentage}% indirim</strong> kazandınız!
                        </p>
                        <p>İndirim kodunuz:</p>
                        <div class='discount-code'>
                            {discountCode}
                        </div>
                        <p style='margin: 25px 0;'>
                            Bu kodu online siparişlerinizde veya restoranımızda kullanabilirsiniz.
                        </p>
                        <a href='https://www.foodu.com/menu' class='button'>MENÜYÜ İNCELE</a>
                    </div>
                    <div class='footer'>
                        <p>© 2025 Foodu Restaurant. Tüm hakları saklıdır.</p>
                        <p>
                            Bu e-postayı almak istemiyorsanız, 
                            <a href='#' style='color: #EB0029;'>abonelikten çıkın</a>.
                        </p>
                    </div>
                </div>
            </body>
            </html>";
        }

        // Geçici metot - bunu sonra SubscriberService'e taşıyacağız
        private async Task<List<Subscriber>> GetActiveSubscribersAsync()
        {
            // Bu metodu sonra SubscriberService'den alacağız
            return new List<Subscriber>();
        }
    }
}