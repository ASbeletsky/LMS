using Autofac;
using Autofac.Core;
using LMS.Data;
using LMS.Data.Migrations;
using LMS.Interfaces;
using LMS.Business.Services;

namespace LMS.Bootstrap
{
    public class DependencyContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutoMapper>()
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AspNetConfigReader>()
                .As<IConfigReader>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LMSDbContext>()
                .AsSelf()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "connection",
                    (pi, ctx) => ctx.Resolve<IConfigReader>().GetConnectionString("DefaultConnection")))
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityFrameworkUnitOfWork>().As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionTypeService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterBuildCallback(container =>
                DbContextDesignFactory.RegisterDbContextFactory(() => container.Resolve<LMSDbContext>()));
        }
    }
}
