using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System;
using LMS.Entities;
using Task = System.Threading.Tasks.Task;
using System.Threading.Tasks;
using LMS.Dto;
using LMS.Business.Services;
using LMS.Interfaces;

namespace LMS.Identity
{
   public class IdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; // allows  to authenticate a user and install or delete his cookies
        private RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _userManager.Users.ToArray();
            var roles = await Task.WhenAll(users.Select(u => _userManager.GetRolesAsync(u)));
            var usersDTO = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users).ToArray();

            for(var i = 0; i < usersDTO.Length; i ++)
            {
                usersDTO[i].Roles = roles[i];
            }
            return usersDTO;
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

        public async Task<IEnumerable<User>> GetAllAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task Logout()
        {
            // delete cookies
            await _signInManager.SignOutAsync();
        }

        public async Task DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if(RoleInitializer.adminUserName == user.UserName)
                    throw new Exception("Sorry, but you can`t delete admin.");
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
        }

        public Task<User> GetById(string id)
        {
            return _userManager.FindByIdAsync(id);
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
