using HizliSec.DataAccess.Concrete.EntityFrameworkCore.Mappings;
using HizliSec.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HizliSec.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class HizliSecContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public HizliSecContext(DbContextOptions<HizliSecContext> options) : base(options)
        {

        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = DESKTOP-CHSJJ4J\\SQLEXPRESS;Initial Catalog=HizliSec;Integrated Security=true;TrustServerCertificate=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new CustomerMap());
            builder.ApplyConfiguration(new OrderDetailMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new SellerMap());
            builder.ApplyConfiguration(new SellerProductMap());
            builder.ApplyConfiguration(new AppUserMap());
            base.OnModelCreating(builder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set;}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SellerProduct> SellersProducts { get; set;}

    }
}
