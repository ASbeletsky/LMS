using LMS.Data;
using LMS.Entries.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Identity
{
   public static class IdentityStartup
    {
        public static void AddIdentity(this IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequireDigit = true;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 3;
            })
             .AddEntityFrameworkStores<LMSDbContext>();
        }
    }
    
}
