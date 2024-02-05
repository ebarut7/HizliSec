using HizliSec.Core.Entities;
using Microsoft.AspNetCore.Identity;


namespace HizliSec.Entities.Concrete
{
    public class AppUser :IdentityUser<int> ,IEntity
    {
        public Customer Customer { get; set; }
        public Seller Seller { get; set; }
    }
}
