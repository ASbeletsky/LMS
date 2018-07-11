using Autofac;
using LMS.Data;
using LMS.Interfaces;

namespace LMS.Bootstrap
{
    public class DependencyContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AspNetConfigReader>().As<IConfigReader>()
                .InstancePerLifetimeScope();

            builder.Register(services => 
                new LMSDbContext(services.Resolve<IConfigReader>().GetConnectionString("DefaultConnection")))
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityFrameworkUnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
