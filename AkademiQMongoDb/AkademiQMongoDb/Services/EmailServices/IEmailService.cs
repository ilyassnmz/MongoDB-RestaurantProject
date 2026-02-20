using AkademiQMongoDb.DTOs.MailDtos;

namespace AkademiQMongoDb.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto emailDto);
        Task SendBulkEmailAsync(List<string> recipients, string subject, string body);
        Task SendDiscountEmailToSubscribersAsync(string discountCode, int discountPercentage);
    }
}