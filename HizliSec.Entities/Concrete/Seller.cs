using HizliSec.Core.Entities;
using HizliSec.Entities.Abstract;


namespace HizliSec.Entities.Concrete
{
    public class Seller : MyUser,IEntity
    {
        public string CompanyName { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<SellerProduct> SellerProducts { get; set; } 
    }
}
