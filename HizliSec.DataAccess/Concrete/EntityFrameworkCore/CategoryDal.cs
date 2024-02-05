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
    public class CategoryDal : RepositoryBase<Category>,ICategoryDal
    {
        public CategoryDal(HizliSecContext context):base(context)
        {
                
        }
    }
}
