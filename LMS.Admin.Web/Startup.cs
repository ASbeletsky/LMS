using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LMS.Socket;
using LMS.Identity;

namespace LMS.Admin.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentity();
            if (Environment.IsDevelopment())
            {
                services.AddSocket();
            }
            else
            {
                services.AddSocket("http://localhost:49241");
            }

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyModules(Assembly.Load("LMS.Bootstrap"));
            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseSocket();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=TestSession}/{action=List}/{id?}");
            });
            RoleInitializer.CreateUsersRoles(serviceProvider).GetAwaiter().GetResult();
        }
    }
}
