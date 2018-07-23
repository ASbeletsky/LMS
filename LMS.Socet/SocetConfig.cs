using Microsoft.AspNetCore.Builder;
using Microsoft.Owin.Cors;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Socet
{
    public static class SocetConfig
    {
        public static void AddSocetConfig(this IApplicationBuilder app)
        {
            app.UseCookiePolicy();
            app.UseCors("AllowAllOrigins");
            //    options => {
            //    //options.WithOrigins("http://localhost:49241").AllowAnyMethod().AllowAnyHeader();
            //    options.WithOrigins("http://localhost:49241").AllowAnyMethod().AllowAnyHeader();
            //    options.DisallowCredentials();
            //});

            app.UseSignalR(routes =>
            {
                routes.MapHub<TestHub>("/testHub");
            });

            //app.Map("/testHub", map =>
            //{
            //    map.UseCors("AllowAllOrigins");
            //    var hubConfiguration = new HubConfiguration { };
            //    map.RunSignalR(hubConfiguration);
            //});

        }
    }
}
