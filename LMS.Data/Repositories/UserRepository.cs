using LMS.Data.Models;
using LMS.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Data.Repositories
{
    class UserRepository : IRepository<User>
    {
        private readonly LMSDbContext dbContext;

        public UserRepository(LMSDbContext context)
        {
            dbContext = context;
        }

        public void Create(User item)
        {
            dbContext.Users.Add(item);
        }

        public void Delete(int id)
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

        public void Update(User item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
