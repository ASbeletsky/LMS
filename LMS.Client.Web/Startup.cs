using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LMS.Identity;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace LMS.Client.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentity();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterAssemblyModules(Assembly.Load("LMS.Bootstrap"));
            var container = builder.Build();

            return new AutofacServiceProvider(container);
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins",
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin().AllowAnyMethod();
            //        });
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //RoleInitializer.CreateUsersRoles(serviceProvider).GetAwaiter().GetResult();
            //app.UseCors("AllowAllOrigins");
        }
    }
}
