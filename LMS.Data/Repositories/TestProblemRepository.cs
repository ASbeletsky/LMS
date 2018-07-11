using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Interfaces;
using LMS.Entities;

namespace LMS.Data.Repositories
{
    public class TestProblemRepository : IRepository<TestProblem>
    {
        private readonly DbSet<TestProblem> set;

        public TestProblemRepository(LMSDbContext context)
        {
            set = context.Set<TestProblem>();
        }

        public void Create(TestProblem item)
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

        public IEnumerable<TestProblem> Filter(Func<TestProblem, bool> predicate)
        {
            return set
                .Include(p => p.Choices)
                .Include(p => p.Type)
                .Where(predicate);
        }

        public TestProblem Find(Func<TestProblem, bool> predicate)
        {
            return set
                .Include(p => p.Choices)
                .Include(p => p.Type)
                .FirstOrDefault(predicate);
        }

        public TestProblem Get(int id)
        {
            return set
                .Include(p => p.Choices)
                .Include(p => p.Type)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<TestProblem> GetAll()
        {
            return set
                .Include(p => p.Choices)
                .Include(p => p.Type);
        }

        public void Update(TestProblem item)
        {
            set.Update(item);
        }
    }
}
