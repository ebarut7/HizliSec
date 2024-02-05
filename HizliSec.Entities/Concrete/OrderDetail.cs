

using HizliSec.Core.Entities;

namespace HizliSec.Entities.Concrete
{
    public class OrderDetail : IEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity 
        {
            get;set;

        }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
