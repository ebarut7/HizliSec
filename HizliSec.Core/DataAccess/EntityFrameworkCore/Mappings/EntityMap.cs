using HizliSec.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HizliSec.Core.DataAccess.EntityFrameworkCore.Mappings
{
    public class EntityMap<T> : IEntityTypeConfiguration<T> where T : class,IEntity,new()
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
        }
    }
}
