using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Data.Repositories
{
    public class TestTemplateRepository : IRepository<TestTemplate>
    {
        private readonly DbSet<TestTemplate> set;
        private readonly LMSDbContext dbContext;
                
        
        public TestTemplateRepository(LMSDbContext context)
        {
            dbContext = context;
            set = context.Set<TestTemplate>();
        }

        public IEnumerable<TestTemplate> GetAll()
        {
            return set
                .Include(t => t.Levels)
                .ThenInclude(l => l.Categories)
                .ThenInclude(c => c.Category)
                .Include(t => t.Levels)
                .ThenInclude(l => l.TaskTypes)
                .ThenInclude(t => t.TaskType);
        }

        public IEnumerable<TestTemplate> Filter(Func<TestTemplate, bool> predicate)
        {
            return GetAll()
                .Where(predicate);
        }

        public TestTemplate Get(int id)
        {
            return Filter(t => t.Id == id)
                .FirstOrDefault();
        }

        public TestTemplate Find(Func<TestTemplate, bool> predicate)
        {
            return Filter(predicate)
                .FirstOrDefault();
        }

        public void Create(TestTemplate item)
        {
            set.Add(item);
        }

        public void Update(TestTemplate item)
        {
            set.Update(item);
        }

        public void Delete(int id)
        {
            var item = set.Find(id);
            if (item != null)
            {
                set.Remove(item);
            }
        }
    }
}
