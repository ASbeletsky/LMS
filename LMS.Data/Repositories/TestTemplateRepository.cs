using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Data.Repositories
{
    public class TestTemplateRepository : IRepository<TestTemplate>
    {
        private readonly DbSet<TestTemplate> set;

        public TestTemplateRepository(LMSDbContext context)
        {
            set = context.Set<TestTemplate>();
        }

        public IEnumerable<TestTemplate> GetAll()
        {
            return GetAllQuery();
        }

        public IEnumerable<TestTemplate> Filter(Expression<Func<TestTemplate, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate);
        }

        public TestTemplate Get(int id)
        {
            return GetAllQuery()
                .FirstOrDefault(t => t.Id == id);
        }

        public TestTemplate Find(Expression<Func<TestTemplate, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
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

        private IQueryable<TestTemplate> GetAllQuery()
        {
            return set
                .Include(t => t.Levels)
                .ThenInclude(l => l.Categories)
                .ThenInclude(c => c.Category)
                .Include(t => t.Levels)
                .ThenInclude(l => l.TaskTypes)
                .ThenInclude(t => t.TaskType);
        }
    }
}
