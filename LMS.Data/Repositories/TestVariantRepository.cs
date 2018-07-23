using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using LMS.Entities;
using LMS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories
{
    public class TestVariantRepository : IRepository<TestVariant>
    {
        private readonly DbContext dbContext;

        public TestVariantRepository(DbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<TestVariant> GetAll()
        {
            return GetAllQuery()
                .ToList();
        }

        public IEnumerable<TestVariant> Filter(Expression<Func<TestVariant, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public TestVariant Get(int id)
        {
            return GetAllQuery()
                .FirstOrDefault(t => t.Id == id);
        }

        public TestVariant Find(Expression<Func<TestVariant, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .FirstOrDefault();
        }

        public void Create(TestVariant item)
        {
            dbContext.Set<TestVariant>().Add(item);
        }

        public void Update(TestVariant item)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var levelsSet = dbContext.Set<TestVariantLevel>();
                var tasksSet = dbContext.Set<TestVariantLevelTask>();

                var entry = dbContext.Set<TestVariant>().Update(item);

                foreach (var level in item.Levels)
                {
                    level.TestVariantId = item.Id;

                    var newLevelTypes = level.Tasks.ToArray();
                    var oldLevelTypes = tasksSet.Where(t => t.LevelId == level.Id).ToArray();
                    tasksSet.RemoveRange(oldLevelTypes.Except(newLevelTypes));
                    tasksSet.AddRange(newLevelTypes.Except(oldLevelTypes));
                }

                var newLevels = item.Levels.ToArray();
                var toRemoveLevels = levelsSet.Where(l => l.TestVariantId == item.Id).Except(newLevels);
                levelsSet.RemoveRange(toRemoveLevels);

                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<TestVariant>().Find(id);
            if (item != null)
            {
                dbContext.Set<TestVariant>().Remove(item);
            }
        }

        private IQueryable<TestVariant> GetAllQuery()
        {
            return dbContext.Set<TestVariant>()
                .Include(t => t.Levels)
                .ThenInclude(l => l.Tasks);
        }
    }
}
