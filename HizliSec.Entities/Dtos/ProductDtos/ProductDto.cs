

using HizliSec.Core.Entities;

namespace HizliSec.Entities.Dtos.ProductDtos
{
    public class ProductDto : IDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }
        public string Description { get; set; }
    }
}
