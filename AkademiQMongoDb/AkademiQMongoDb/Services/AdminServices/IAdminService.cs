using AkademiQMongoDb.DTOs.AdminDtos;

namespace AkademiQMongoDb.Services.AdminServices
{
    public interface IAdminService
    {
        Task CreateAdminAsync(RegisterAdminDto registerAdminDto);
        Task<bool> LoginAdminAsync(LoginAdminDto loginAdminDto);
        Task<ResultAdminDto> GetAdminByUserNameAsync(string userName);

    }
}