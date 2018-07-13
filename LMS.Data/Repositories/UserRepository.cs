using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Interfaces;
using LMS.Entities;


namespace LMS.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly LMSDbContext dbContext;

        public UserRepository(LMSDbContext context)
        {
            dbContext = context;
        }

        public void CreateAsync(User item)
        {
            dbContext.Users.Add(item);
        }

        public void DeleteAsync(int id)
        {
            User usr = dbContext.Users.Find(id);
            if (usr != null)
                dbContext.Users.Remove(usr);
        }

        public IEnumerable<User> Filter(Func<User, bool> predicate)
        {
            return dbContext.Users.Where(predicate);
        }

        public User Find(Func<User, bool> predicate)
        {
            return dbContext.Users.FirstOrDefault(predicate);
        }

        public User Get(int id)
        {
            return dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return dbContext.Users;
        }

        public void UpdateAsync(User item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
