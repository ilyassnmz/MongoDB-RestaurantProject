using AkademiQMongoDb.DTOs.CategoryDtos;

namespace AkademiQMongoDb.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllAsync();
        Task<UpdateCategoryDto> GetByIdAsync(string id);
        Task CreateAsync(CreateCategoryDto categoryDto);
        Task UpdateAsync(UpdateCategoryDto categoryDto);
        Task DeleteAsync(string id);
    }
}
