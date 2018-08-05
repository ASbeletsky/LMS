using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Socket
{
    public static class SocketStartup
    {
        public static void AddSocket(this IServiceCollection services, string clientOrigin = null)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors(options =>
            {
                if (clientOrigin == null)
                {
                    options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin());
                }
                else
                {
                    options.AddPolicy("AllowAllOrigins", builder => builder.WithOrigins(clientOrigin)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                }
            });

            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
        }
    }
}
