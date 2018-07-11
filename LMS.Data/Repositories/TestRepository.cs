using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Interfaces;
using LMS.Data.Models;

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
                .Include(p => p.Problems)
                .Include(p => p.Category)
                .Where(predicate);
        }

        public Test Find(Func<Test, bool> predicate)
        {
            return set
                .Include(p => p.Problems)
                .Include(p => p.Category)
                .FirstOrDefault(predicate);
        }

        public Test Get(int id)
        {
            return set
                .Include(p => p.Problems)
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Test> GetAll()
        {
            return set
                .Include(p => p.Problems)
                .Include(p => p.Category);
        }

        public void Update(Test item)
        {
            set.Update(item);
        }
    }
}
