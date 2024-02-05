using HizliSec.Core.DataAccess.EntityFrameworkCore.Mappings;
using HizliSec.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore.Mappings
{
    public class CategoryMap:EntityMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasKey(x => x.Id);
            
            builder.HasMany(c => c.Products).WithOne(c => c.Category).HasForeignKey(c => c.CategoryId);
            base.Configure(builder);
        }
    }
}
