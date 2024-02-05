using HizliSec.Core.Entities;


namespace HizliSec.Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set;}
        public decimal Price { get; set; }
        
        public int StockAmount
        {
            get;
            set;
        }
        public string Description { get; set; }

        public Category Category { get; set; }
        public ICollection<SellerProduct> SellerProducts { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
