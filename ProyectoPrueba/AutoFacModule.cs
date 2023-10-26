using Autofac;
using ProyectoPrueba.Factories;
using ProyectoPrueba.Models;

namespace ProyectoPrueba
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductContext>().As<ProductContext>();
            builder.Register(ctx =>
            {
                var allContext = new Dictionary<string, ProductContext>();
                allContext.Add("Product1", ctx.Resolve<ProductContext>());
                allContext.Add("Product2", ctx.Resolve<Product2Context>());

                return new ContextFactory(allContext);

            });

        }
    }
}
