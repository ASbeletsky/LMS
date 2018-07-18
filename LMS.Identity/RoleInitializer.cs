using LMS.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using Task = System.Threading.Tasks.Task;

namespace LMS.Identity
{
    public static class RoleInitializer
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            string adminEmail = "admin@gmail.com";
            string password = "apriorit";
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (!await roleManager.RoleExistsAsync("moderator"))
            {
                await roleManager.CreateAsync(new IdentityRole("moderator"));
            }
            if (!await roleManager.RoleExistsAsync("reviewer"))
            {
                await roleManager.CreateAsync(new IdentityRole("reviewer"));
            }
            if (!await roleManager.RoleExistsAsync("examinee"))
            {
                await roleManager.CreateAsync(new IdentityRole("examinee"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new User { Email = adminEmail, UserName = adminEmail };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}
