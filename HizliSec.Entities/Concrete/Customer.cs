using HizliSec.Core.Entities;
using HizliSec.Entities.Abstract;
namespace HizliSec.Entities.Concrete
{
    public class Customer : MyUser ,IEntity
    {
        public AppUser AppUser { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
