

using HizliSec.Entities.Dtos.CategoryDtos;

namespace HizliSec.Business.Abstract
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetByIdAsync(int id);
        Task<List<CategoryDto>> GetByNameFilterAsync(string filter = "");
        Task<List<CategoryDto>> GetAllAsync();
        Task<int> DeleteAsync(int id);
        Task<int> AddAsync(CategoryAddDto categoryDto);
        Task<int> UpdateAsync(CategoryDto categoryDto);

    }
}
