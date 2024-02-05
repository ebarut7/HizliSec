using HizliSec.Business.Abstract;
using HizliSec.Entities.Dtos.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HizliSec.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;

        public ProductsController(IProductService productService, IAuthService authService)
        {
            _productService = productService;
            _authService = authService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ProductDto productDto = await _productService.GetByIdAsync(id);
            return Ok(productDto);
        }

        [Authorize(Roles = "CUSTOMER")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<ProductDto> productDtos = await _productService.GetAllAsync();
            return productDtos is not null ? Ok(productDtos) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productDto,string userName)
        {
            int response = await _productService.AddAsync(productDto, userName);
            return response > 0 ? Ok("Başarılı bir şekilde eklendi.") : NotFound("Bir problem oluştu!");
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
           int response = await _productService.UpdateAsync(productDto);
            return response > 0 ? Ok("Başarılı bir şekilde güncellendi.") : NotFound("Bir problem oluştu!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            int response = await _productService.DeleteAsync(id);
            return response > 0 ? Ok("Veri silindi.") : NotFound("Bir problem oluştu!");
        }
    }
}
