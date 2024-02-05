using HizliSec.Core.DataAccess.EntityFrameworkCore.Mappings;
using HizliSec.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class CustomerMap : EntityMap<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(25);

            builder.HasMany(x => x.Orders).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);
           
            base.Configure(builder);
        }
    }
}
