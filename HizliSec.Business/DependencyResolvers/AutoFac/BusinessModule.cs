

using Autofac;
using HizliSec.Business.Abstract;
using HizliSec.Business.Concrete;

namespace HizliSec.Business.DependencyResolvers.AutoFac
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<OrderProcessManager>().As<IOrderProcessService>();
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            base.Load(builder);
        }
    }
}
