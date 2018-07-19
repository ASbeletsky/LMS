using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using System;
using LMS.Entities;
using System.Collections.Generic;

namespace LMS.Identity
{
   public class IdentityService 
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; // allows  to authenticate a user and install or delete his cookies
        private RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<bool> Register(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role); // add role
            }
            else
                throw new AggregateException(result.Errors.Select(error => new Exception(error.Description)));

            return result.Succeeded;
        }
        public async Task<bool> LogIn(string userName, string password, bool rememberMe)
        {
            var result =
            await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);

            return result.Succeeded;
        }

        public async System.Threading.Tasks.Task Logout()
        {
            // delete cookies
            await _signInManager.SignOutAsync();
        }

    }
}
