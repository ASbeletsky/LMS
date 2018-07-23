using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace LMS.Socet
{
    public static class SocetStartup
    {
        public static void AddSocet(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors(options =>
            {
                {
                    //var corsPolicy = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy();
                    //corsPolicy.SupportsCredentials = true;
                    options.AddPolicy("AllowAllOrigins", builder => builder.WithOrigins("http://localhost:49241")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
                    //builder =>
                    //{
                    //    builder. AllowAnyOrigin().AllowAnyMethod().DisallowCredentials();
                    //});
                }
            });

            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
        }
    }
}
