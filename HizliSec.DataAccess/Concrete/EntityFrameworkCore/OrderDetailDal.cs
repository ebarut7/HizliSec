using HizliSec.Core.DataAccess.EntityFrameworkCore;
using HizliSec.DataAccess.Abstract;
using HizliSec.DataAccess.Concrete.EntityFrameworkCore.Context;
using HizliSec.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore
{
    public class OrderDetailDal:RepositoryBase<OrderDetail>,IOrderDetailDal
    {
        public OrderDetailDal(HizliSecContext context) : base(context)
        {
            
        }
    }
}
