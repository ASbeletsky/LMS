using LMS.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using Task = System.Threading.Tasks.Task;

namespace LMS.Identity
{
    public static class RoleInitializer
    {
        public const string adminUserName = "admin";
        private const string password = "apriorit";
        public static async Task CreateUsersRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Moderator))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Moderator));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Reviewer))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Reviewer));
            }
            if (!await roleManager.RoleExistsAsync(Roles.Examinee))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Examinee));
            }

            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                var admin = new User {UserName = adminUserName };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
            }
        }
    }
}
