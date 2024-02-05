using HizliSec.Business.Abstract;
using HizliSec.Entities.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;

namespace HizliSec.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            CategoryDto categoryDto = await _categoryService.GetByIdAsync(id);
            return categoryDto is not null ? Ok(categoryDto) : BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<CategoryDto> categoryDtos =  await _categoryService.GetAllAsync();
            return categoryDtos is null ? BadRequest() : Ok(categoryDtos);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            int response = await _categoryService.DeleteAsync(id);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            int response = await _categoryService.AddAsync(categoryAddDto);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            int response = await _categoryService.UpdateAsync(categoryDto);
            return response > 0 ? Ok("Guncelleme islemi basarili bir sekilde tamamlandi.") : BadRequest("Bir problem olustu!");
        }
    }
}
