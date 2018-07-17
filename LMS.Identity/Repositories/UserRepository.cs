using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using LMS.Interfaces;
using LMS.Entities;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<User>> FilterAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userManager.Users.Where(predicate).ToListAsync();
        }

        public async Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userManager.Users.FirstOrDefaultAsync(predicate);

        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public Task<User> GetAsync(int id)
        {
            return _userManager.FindByIdAsync(id.ToString());
        }
    }
}
