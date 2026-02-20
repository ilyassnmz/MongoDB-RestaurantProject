using AkademiQMongoDb.DTOs.SubscriberDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using Mapster;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.SubscriberServices
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IMongoCollection<Subscriber> _subscriberCollection;

        public SubscriberService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _subscriberCollection = database.GetCollection<Subscriber>("Subscribers");
        }

        public async Task<List<ResultSubscriberDto>> GetAllAsync()
        {
            var subscribers = await _subscriberCollection.Find(x => true).ToListAsync();
            return subscribers.Adapt<List<ResultSubscriberDto>>();
        }

        public async Task<List<ResultSubscriberDto>> GetActiveSubscribersAsync()
        {
            var subscribers = await _subscriberCollection.Find(x => x.IsActive).ToListAsync();
            return subscribers.Adapt<List<ResultSubscriberDto>>();
        }

        public async Task CreateAsync(CreateSubscriberDto createSubscriberDto)
        {
            var subscriber = createSubscriberDto.Adapt<Subscriber>();
            subscriber.SubscribeDate = DateTime.Now;
            subscriber.IsActive = true;
            await _subscriberCollection.InsertOneAsync(subscriber);
        }

        public async Task DeleteAsync(string id)
        {
            await _subscriberCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var subscriber = await _subscriberCollection.Find(x => x.Email == email).FirstOrDefaultAsync();
            return subscriber != null;
        }
    }
}