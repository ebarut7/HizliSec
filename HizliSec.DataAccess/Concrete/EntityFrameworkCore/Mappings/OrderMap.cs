using HizliSec.Core.DataAccess.EntityFrameworkCore.Mappings;
using HizliSec.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class OrderMap : EntityMap<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.HasMany(x => x.OrderDetails).WithOne(x => x.Order).HasForeignKey(x=>x.OrderId);

            base.Configure(builder);
        }
    }
}
