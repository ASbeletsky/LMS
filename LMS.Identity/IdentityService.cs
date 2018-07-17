using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using System;
using LMS.Entities;

namespace LMS.Identity
{
   public class IdentityService 
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; // allows  to authenticate a user and install or delete his cookies

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Register(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
                await _signInManager.SignInAsync(user, false);  // set cookies
            else
                throw new AggregateException(result.Errors.Select(error => new Exception(error.Description)));

            return result.Succeeded;
        }
        public async Task<bool> LogIn(string email, string password, bool rememberMe)
        {
            var result =
            await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);

            return result.Succeeded;
        }

        public async System.Threading.Tasks.Task Logout()
        {
            // delete cookies
            await _signInManager.SignOutAsync();
        }

    }
}
