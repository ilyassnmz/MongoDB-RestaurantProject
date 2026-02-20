using AkademiQMongoDb.DTOs.SubscriberDtos;

namespace AkademiQMongoDb.Services.SubscriberServices
{
    public interface ISubscriberService
    {
        Task<List<ResultSubscriberDto>> GetAllAsync();
        Task<List<ResultSubscriberDto>> GetActiveSubscribersAsync();
        Task CreateAsync(CreateSubscriberDto createSubscriberDto);
        Task DeleteAsync(string id);
        Task<bool> IsEmailExistsAsync(string email);
    }
}