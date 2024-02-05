using HizliSec.Core.DataAccess.EntityFrameworkCore.Mappings;
using HizliSec.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class SellerProductMap: EntityMap<SellerProduct>
    {
        public override void Configure(EntityTypeBuilder<SellerProduct> builder)
        {
            builder.HasKey(x => new { x.SellerId, x.ProductId });

            base.Configure(builder);
        }
    }
}
