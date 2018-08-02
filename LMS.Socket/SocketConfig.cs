using Microsoft.AspNetCore.Builder;

namespace LMS.Socket
{
    public static class SocketConfig
    {
        public static void UseSocket(this IApplicationBuilder app)
        {
            app.UseCookiePolicy();
            app.UseCors("AllowAllOrigins");

            app.UseSignalR(routes =>
            {
                routes.MapHub<TestHub>("/testHub");
            });
        }
    }
}
