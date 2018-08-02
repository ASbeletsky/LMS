using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System;
using LMS.Entities;
using Task = System.Threading.Tasks.Task;

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

        public async Task Register(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role); // add role
            }
            else
                throw new AggregateException(result.Errors.Select(error => new Exception(error.Description)));
        }

        public async Task LogIn(string userName, string password, bool rememberMe)
        {
            User user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new Exception("The username or password provided is incorrect.");
            var currentUserRole = await _userManager.GetRolesAsync(user);
            if (currentUserRole.Contains(Roles.Admin) 
                || currentUserRole.Contains(Roles.Moderator) 
                || currentUserRole.Contains(Roles.Reviewer))
            {
                var result =
                    await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
                if(!result.Succeeded)
                    throw new Exception("The username or password provided is incorrect.");
            }
            else
                throw new Exception("Your role does not allow you to enter.");
        }

        public async System.Threading.Tasks.Task<IEnumerable<User>> GetAllAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task Logout()
        {
            // delete cookies
            await _signInManager.SignOutAsync();
        }

    }
}
