using HizliSec.Core.DataAccess.EntityFrameworkCore.Mappings;
using HizliSec.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class SellerMap:EntityMap<Seller>
    {
        public override void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.CompanyName).IsRequired();

            base.Configure(builder);
        }
    }
}
