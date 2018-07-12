using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Data.Repositories
{
    public class TestRepository : IRepository<Test>
    {
        private readonly DbSet<Test> set;

        public TestRepository(LMSDbContext context)
        {
            set = context.Set<Test>();
        }

        public void Create(Test item)
        {
            set.Add(item);
        }

        public void Delete(int id)
        {
            var item = set.Find(id);
            if (item != null)
            {
                set.Remove(item);
            }
        }

        public IEnumerable<Test> Filter(Func<Test, bool> predicate)
        {
            return set
                .Include(t => t.Questions)
                .Include(t => t.Category)
                .Where(predicate);
        }

        public Test Find(Func<Test, bool> predicate)
        {
            return set
                .Include(t => t.Questions)
                .Include(t => t.Category)
                .FirstOrDefault(predicate);
        }

        public Test Get(int id)
        {
            return set
                .Include(t => t.Questions)
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Test> GetAll()
        {
            return set
                .Include(t => t.Questions)
                .Include(t => t.Category);
        }

        public void Update(Test item)
        {
            set.Update(item);
        }
    }
}
