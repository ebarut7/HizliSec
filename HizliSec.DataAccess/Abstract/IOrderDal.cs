using HizliSec.Core.DataAccess;
using HizliSec.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HizliSec.DataAccess.Abstract
{
    public interface IOrderDal : IRepositoryBase<Order>
    {
    }
}
