using HizliSec.Core.DataAccess.EntityFrameworkCore;
using HizliSec.DataAccess.Abstract;
using HizliSec.DataAccess.Concrete.EntityFrameworkCore.Context;
using HizliSec.Entities.Concrete;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore
{
    public class SellerProductDal : RepositoryBase<SellerProduct>,ISellerProductDal
    {
        public SellerProductDal(HizliSecContext context) : base(context)
        {
               
        }
    }
}
