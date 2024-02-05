using HizliSec.DataAccess.Abstract;
using HizliSec.DataAccess.Concrete.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace HizliSec.Business.DependencyResolvers.Extension
{
    public static class Container
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.AddScoped<ICategoryDal, CategoryDal>();
            services.AddScoped<ICustomerDal,CustomerDal>();
            services.AddScoped<IOrderDal,OrderDal>();
            services.AddScoped<IOrderDetailDal,OrderDetailDal>();
            services.AddScoped<IProductDal,ProductDal>();
            services.AddScoped<ISellerDal,SellerDal>();
            services.AddScoped<ISellerProductDal,SellerProductDal>();
            return services;
        }
    }
}
