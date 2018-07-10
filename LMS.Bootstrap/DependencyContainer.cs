using Autofac;
using LMS.Data;
using LMS.Interfaces;

namespace LMS.Bootstrap
{
    public class DependencyContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LMSDbContext>().SingleInstance();
            builder.RegisterType<EntityFrameworkUnitOfWork>().As<IUnitOfWork>();
        }
    }
}
