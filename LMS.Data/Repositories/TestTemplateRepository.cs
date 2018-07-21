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
        private readonly DbContext dbContext;

        public TestTemplateRepository(DbContext context)
        {
            dbContext = context;
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
            dbContext.Set<TestTemplate>().Add(item);
        }

        public void Update(TestTemplate item)
        {
            var levelsSet = dbContext.Set<TestTemplateLevel>();
            var categoriesSet = dbContext.Set<LevelCategory>();
            var typesSet = dbContext.Set<LevelTaskType>();

            var entry = dbContext.Set<TestTemplate>().Update(item);

            foreach (var level in item.Levels)
            {
                level.TestTemplateId = item.Id;

                var newLevelTypes = level.TaskTypes.ToArray();
                var oldLevelTypes = typesSet.Where(t => t.TestTemplateLevelId == level.Id).ToArray();
                typesSet.RemoveRange(oldLevelTypes.Except(newLevelTypes));
                typesSet.AddRange(newLevelTypes.Except(oldLevelTypes));

                var newLevelCategories = level.Categories.ToArray();
                var oldLevelCategories = categoriesSet.Where(t => t.TestTemplateLevelId == level.Id).ToArray();
                categoriesSet.RemoveRange(oldLevelCategories.Except(newLevelCategories));
                categoriesSet.AddRange(newLevelCategories.Except(oldLevelCategories));
            }

            var newLevels = item.Levels.ToArray();
            var toRemoveLevels = levelsSet.Where(l => l.TestTemplateId == item.Id).Except(newLevels);
            levelsSet.RemoveRange(toRemoveLevels);
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<TestTemplate>().Find(id);
            if (item != null)
            {
                dbContext.Set<TestTemplate>().Remove(item);
            }
        }

        private IQueryable<TestTemplate> GetAllQuery()
        {
            return dbContext.Set<TestTemplate>()
                .Include(t => t.Levels)
                .ThenInclude(l => l.Categories)
                .ThenInclude(c => c.Category)
                .Include(t => t.Levels)
                .ThenInclude(l => l.TaskTypes)
                .ThenInclude(t => t.TaskType);
        }
    }
}
