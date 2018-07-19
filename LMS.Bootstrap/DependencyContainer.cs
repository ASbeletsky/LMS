using Autofac;
using Autofac.Core;
using LMS.Identity;
using LMS.Data;
using LMS.Data.Migrations;
using LMS.Interfaces;
using LMS.Business.Services;
using LMS.Identity.Repositories;
using LMS.Entities;

namespace LMS.Bootstrap
{
    public class DependencyContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapping.AutoMapper>()
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

            builder.RegisterType<UserRepository>()
                .As<IRepositoryAsync<User>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<EntityFrameworkUnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdentityService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<TaskService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<TaskTypeService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterBuildCallback(container =>
                DbContextDesignFactory.RegisterDbContextFactory(() => container.Resolve<LMSDbContext>()));
        }
    }
}
