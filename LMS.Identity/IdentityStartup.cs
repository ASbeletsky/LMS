using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LMS.Entities;

namespace LMS.Identity
{
   public static class IdentityStartup
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 3;
            })
            .AddEntityFrameworkStores<DbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/Login";
            });
        }
    }
}
    

