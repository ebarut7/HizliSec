using HizliSec.Core.DataAccess.EntityFrameworkCore.Mappings;
using HizliSec.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class ProductMap : EntityMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x=>x.Id);


            builder.HasMany(p => p.OrderDetails).WithOne(x => x.Product).HasForeignKey(x=>x.ProductId);

            base.Configure(builder);
        }
    }
}
