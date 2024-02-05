
using HizliSec.Core.Entities;

namespace HizliSec.Entities.Dtos.SellerProductDtos
{
    public class SellerProductDto : IDto
    {
        public int SellerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
