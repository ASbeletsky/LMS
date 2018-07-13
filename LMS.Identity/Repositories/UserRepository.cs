using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using LMS.Interfaces;
using LMS.Entities;

namespace LMS.Identity.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async void CreateAsync(User item)
        {
            var result = await _userManager.CreateAsync(item);
        }

        public async void DeleteAsync(int id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
        }

        public IEnumerable<User> Filter(Func<User, bool> predicate)
        {
            return _userManager.Users.Where(predicate);
        }

        public User Find(Func<User, bool> predicate)
        {
            return _userManager.Users.FirstOrDefault(predicate);
        }

        public User Get(int id)
        {
            return _userManager.FindByIdAsync(id.ToString()).GetAwaiter().GetResult();
        }

        public IEnumerable<User> GetAll()
        {
            return _userManager.Users;
        }

        public async void UpdateAsync(User item)
        {
           await _userManager.UpdateAsync(item);
        }
    }
}
