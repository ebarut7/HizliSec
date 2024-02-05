using HizliSec.Business.Abstract;
using HizliSec.Entities.Concrete;
using HizliSec.Entities.Dtos.ProductDtos;

namespace HizliSec.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public ProductManager(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<int> AddAsync(ProductAddDto productDto, string userName)
        {
            Product product =  _unitOfWork.ProductDal.GetAsync(x=>x.Name == productDto.Name).Result;
            AppUser user = await _authService.GetUserAsync(userName);
            SellerProduct sellerProduct;
            if (product is null)
            {
                product = new Product()
                {
                    Description = productDto.Description,
                    Name = productDto.Name,
                    Price = productDto.Price,
                    StockAmount = productDto.StockAmount,
                    CategoryId = productDto.CategoryId
                };
                await _unitOfWork.ProductDal.AddAsync(product);
                var a = await _unitOfWork.SaveAsync();
                sellerProduct = new SellerProduct()
                {
                    SellerId = user.Id,
                    ProductId = product.Id,
                    Quantity = productDto.StockAmount
                };
                await _unitOfWork.SellerProductDal.AddAsync(sellerProduct);
            }
            else
            {
                product.StockAmount += productDto.StockAmount;
                await _unitOfWork.ProductDal.UpdateAsync(product);
                sellerProduct = await _unitOfWork.SellerProductDal.GetAsync(x=>x.ProductId == product.Id && x.SellerId == user.Id);
                sellerProduct.Quantity += productDto.StockAmount;
            }
            
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Product product = await _unitOfWork.ProductDal.GetAsync(p => p.Id == id);
            await _unitOfWork.ProductDal.DeleteAsync(product);
            return await _unitOfWork.SaveAsync();
        }
        public async Task<List<ProductDto>> GetAllByNameFilterAsync(string filter = "")
        {
            List<Product> products = await _unitOfWork.ProductDal.GetAllAsync(x=>x.Name.Contains(filter));
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (Product product in products)
            {
                productDtos.Add(new ProductDto()
                {
                     Name = product.Name,
                      CategoryId = product.CategoryId,
                       Description = product.Description,
                        Id = product.Id,
                         Price = product.Price,
                          StockAmount = product.StockAmount
                });
            }
            return productDtos;
        }
        public async Task<List<ProductDto>> GetAllAsync()
        {
            List<Product> products = await _unitOfWork.ProductDal.GetAllAsync();
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (Product product in products)
            {
                productDtos.Add(new ProductDto()
                {
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Id = product.Id,
                    Price = product.Price,
                    StockAmount = product.StockAmount
                });
            }
            return productDtos;
        }

        public async Task<List<ProductDto>> GetAllOrderByDescPriceAsync()
        {
            return (await GetAllByNameFilterAsync()).OrderByDescending(x=>x.Price).ToList();
        }

        public async Task<List<ProductDto>> GetAllOrderByPriceAsync()
        {
            return (await GetAllByNameFilterAsync()).OrderBy(x => x.Price).ToList();
        }

        public Task<List<ProductDto>> GetAllSellerByIdAsync(int id)
        {
            return null;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
           Product product =  await _unitOfWork.ProductDal.GetAsync(x => x.Id == id);
            return new ProductDto()
            {
                 CategoryId = product.CategoryId,
                  Description = product.Description,
                   Id = product.Id,
                    Name = product.Name,
                     Price = product.Price,
                      StockAmount = product.StockAmount
            };
        }

        public async Task<int> UpdateAsync(ProductDto productDto)
        {
            Product product = await _unitOfWork.ProductDal.GetAsync(x => x.Id == productDto.Id);
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            product.StockAmount = productDto.StockAmount;
            product.CategoryId = productDto.CategoryId;
            product.Name = productDto.Name;
            await _unitOfWork.ProductDal.UpdateAsync(product);
            return await _unitOfWork.SaveAsync();
        }
    }
}
