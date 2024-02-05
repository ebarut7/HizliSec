
using HizliSec.Entities.Dtos.ProductDtos;

namespace HizliSec.Business.Abstract
{
    public interface IProductService
    { 
        Task<ProductDto> GetByIdAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task<List<ProductDto>> GetAllByNameFilterAsync(string filter = "");
        Task<List<ProductDto>> GetAllOrderByPriceAsync();
        Task<List<ProductDto>> GetAllOrderByDescPriceAsync();
        Task<List<ProductDto>> GetAllSellerByIdAsync(int id);
        Task<int> AddAsync(ProductAddDto productDto,string userName);
        Task<int> UpdateAsync(ProductDto productDto);
        Task<int> DeleteAsync(int id);
    }
}
