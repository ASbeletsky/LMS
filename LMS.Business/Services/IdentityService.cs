using LMS.Entries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Business.Services
{
   public class IdentityService 
    {
        private readonly UserManager<User> _userManager; // сервис по управлению пользователями
        private readonly SignInManager<User> _signInManager; //сервис который позволяет аутентифицировать пользователя и устанавливать или удалять его куки

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task  <IEnumerable<IdentityError>> Register(User user, string password)
        {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return Enumerable.Empty<IdentityError>();
                }
                else
                {
                      return result.Errors;
                }
        }
        public async Task<bool> LogIn(string email, string password, bool rememberMe)
        {
            var result =
            await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);

            if (result.Succeeded)
                return true;

            else
                return false;
        }

        public async Task LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
        }

    }
}
