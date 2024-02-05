using HizliSec.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HizliSec.Business.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryDal CategoryDal { get; }
        ICustomerDal CustomerDal { get; }
        IOrderDal OrderDal { get; }
        IOrderDetailDal OrderDetailDal { get; }
        IProductDal ProductDal { get; }
        ISellerDal SellerDal { get; }
        ISellerProductDal SellerProductDal { get; }
        Task<int> SaveAsync();
    }
}
