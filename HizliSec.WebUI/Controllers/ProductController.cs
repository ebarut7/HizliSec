using HizliSec.Business.Abstract;
using HizliSec.Entities.Dtos.CategoryDtos;
using HizliSec.Entities.Dtos.ProductDtos;
using HizliSec.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HizliSec.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        List<ProductVM> productVMs=new();
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            Doldur();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(productVMs);
        }

        [HttpGet]

        public IActionResult Details(int id)
        {
            var productVM=productVMs.FirstOrDefault(x => x.Id == id);
            return View(productVM);
        }

        [HttpGet]

        public async Task<IActionResult> Add()
        {
            List<CategoryDto> categoryDtos = await _categoryService.GetAllAsync();
            return View(categoryDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            string userName = User.Identity.Name;
            int response= await _productService.AddAsync(productAddDto,userName);
            return response > 0 ? RedirectToAction("Index") : View(productAddDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ProductVM productVM = productVMs.FirstOrDefault(x => x.Id == id);
            ViewBag.Categories = _categoryService.GetAllAsync().Result;
            return View(productVM);

        }
        [HttpPost]

        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var res = await _productService.UpdateAsync(productDto);
            return res > 0 ? RedirectToAction("index") : View(productDto);
        }


        [HttpGet]
        public async Task< IActionResult> Delete(int id)
        {
            int response = await _productService.DeleteAsync(id);
            return response > 0 ? RedirectToAction("index"):RedirectToAction("details",new { id = id});
        }
        public async Task Doldur()
        {
            List<ProductDto> productDtos = await _productService.GetAllByNameFilterAsync();
           var category = _categoryService.GetAllAsync().Result;
            foreach (var item in productDtos)
            {
                ProductVM productVM = new ProductVM()
                {
                    Id=item.Id,
                    Name = item.Name,
                    CategoryName = category.FirstOrDefault(x => x.Id == item.CategoryId).Name,
                    Description = item.Description,
                    Price = item.Price,
                    StockAmount = item.StockAmount
                };
                productVMs.Add(productVM);
            }
        }

    }
}
