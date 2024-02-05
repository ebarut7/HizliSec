using HizliSec.Core.Entities;

namespace HizliSec.Entities.Dtos.ProductDtos
{
    public class ProductAddDto : IDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }
        public string Description { get; set; }
    }
}
