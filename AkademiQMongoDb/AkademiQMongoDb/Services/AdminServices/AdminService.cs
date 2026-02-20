using AkademiQMongoDb.DTOs.AdminDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using Mapster;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.AdminServices
{
    public class AdminService : IAdminService
    {
        private readonly IMongoCollection<Admin> _adminCollection;

        public AdminService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _adminCollection = database.GetCollection<Admin>("Admins");
        }

        public async Task CreateAdminAsync(RegisterAdminDto registerAdminDto)
        {
            var admin = registerAdminDto.Adapt<Admin>();
            await _adminCollection.InsertOneAsync(admin);
        }

        public async Task<ResultAdminDto> GetAdminByUserNameAsync(string userName)
        {
            var admin = await _adminCollection.Find(x => x.Username == userName).FirstOrDefaultAsync();

            return admin.Adapt<ResultAdminDto>();
        }

        public async Task<bool> LoginAdminAsync(LoginAdminDto loginAdminDto)
        {
            var admin = await _adminCollection.Find(x => x.Username == loginAdminDto.UserName && x.Password == loginAdminDto.Password && x.IsVerified).FirstOrDefaultAsync();
            if (admin is null)
            {
                return false;
            }

            return true;
        }
    }
}