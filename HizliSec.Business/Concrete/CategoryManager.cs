using HizliSec.Business.Abstract;
using HizliSec.Entities.Concrete;
using HizliSec.Entities.Dtos.CategoryDtos;

namespace HizliSec.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(CategoryAddDto categoryDto)
        {
            Category category = await _unitOfWork.CategoryDal.GetAsync(x=>x.Name == categoryDto.Name);
            if (category is not null)
            {
                return -1;
            }
            await _unitOfWork.CategoryDal.AddAsync(new Category()
            {
                Name = categoryDto.Name
            });
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Category category = await _unitOfWork.CategoryDal.GetAsync(x=>x.Id == id);
            await _unitOfWork.CategoryDal.DeleteAsync(category);
            return await _unitOfWork.SaveAsync();  
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
           var categories = await _unitOfWork.CategoryDal.GetAllAsync();
            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            foreach (var item in categories)
            {
                categoryDtos.Add(new CategoryDto()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return categoryDtos;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            Category category = await _unitOfWork.CategoryDal.GetAsync(x=>x.Id == id);
            return new CategoryDto()
            {
                 Name = category.Name,
                  Id = category.Id
            };
        }

        public async Task<List<CategoryDto>> GetByNameFilterAsync(string filter = "")
        {
            return (await GetAllAsync()).Where(x => x.Name.Contains(filter)).ToList();
        }

        public async Task<int> UpdateAsync(CategoryDto categoryDto)
        {
            Category category = await _unitOfWork.CategoryDal.GetAsync(x=>x.Id == categoryDto.Id);
            category.Name = categoryDto.Name;
            await _unitOfWork.CategoryDal.UpdateAsync(category);
            return await _unitOfWork.SaveAsync();
        }
    }
}
