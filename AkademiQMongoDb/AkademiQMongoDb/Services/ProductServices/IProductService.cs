using AkademiQMongoDb.DTOs.ProductDtos;

namespace AkademiQMongoDb.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllAsync();
        Task<UpdateProductDto> GetByIdAsync(string id);
        Task CreateAsync(CreateProductDto productDto);
        Task UpdateAsync(UpdateProductDto productDto);
        Task DeleteAsync(string id);
    }
}
