using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using LMS.Interfaces;
using LMS.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Identity.Repositories
{
    public class UserRepository : IRepositoryAsync<User>
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public System.Threading.Tasks.Task CreateAsync(User item)
        {
           return _userManager.CreateAsync(item);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public System.Threading.Tasks.Task UpdateAsync(User item)
        {
            return _userManager.UpdateAsync(item);
        }

        public Task<IEnumerable<User>> FilterAsync(Expression<Func<User, bool>> predicate)
        {
            return System.Threading.Tasks.Task.FromResult(_userManager.Users.Where(predicate).AsEnumerable());
        }

        public Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return System.Threading.Tasks.Task.FromResult(_userManager.Users.FirstOrDefault(predicate));
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return System.Threading.Tasks.Task.FromResult(_userManager.Users.AsEnumerable());
        }

        public Task<User> GetAsync(int id)
        {
           return _userManager.FindByIdAsync(id.ToString());
        }



    }
}
