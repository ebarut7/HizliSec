using HizliSec.Core.DataAccess.EntityFrameworkCore;
using HizliSec.DataAccess.Abstract;
using HizliSec.DataAccess.Concrete.EntityFrameworkCore.Context;
using HizliSec.Entities.Concrete;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore
{
    public class ProductDal : RepositoryBase<Product>,IProductDal
    {
        public ProductDal(HizliSecContext context) : base(context)
        {
            
        }
    }
}
