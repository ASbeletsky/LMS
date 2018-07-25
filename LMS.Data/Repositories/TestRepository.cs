using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using LMS.Entities;
using LMS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories
{
    public class TestRepository : IRepository<Test>
    {
        private readonly DbContext dbContext;

        public TestRepository(DbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<Test> GetAll()
        {
            return GetAllQuery()
                .ToList();
        }

        public IEnumerable<Test> Filter(Expression<Func<Test, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public Test Get(int id)
        {
            return GetAllQuery()
                .FirstOrDefault(t => t.Id == id);
        }

        public Test Find(Expression<Func<Test, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .FirstOrDefault();
        }

        public void Create(Test item)
        {
            dbContext.Set<Test>().Add(item);
        }

        public void Update(Test item)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var levelsSet = dbContext.Set<TestLevel>();
                var tasksSet = dbContext.Set<TestLevelTask>();

                var entry = dbContext.Set<Test>().Update(item);

                foreach (var level in item.Levels)
                {
                    level.TestId = item.Id;

                    var newLevelTypes = level.Tasks.ToArray();
                    var oldLevelTypes = tasksSet.Where(t => t.LevelId == level.Id).ToArray();
                    tasksSet.RemoveRange(oldLevelTypes.Except(newLevelTypes));
                    tasksSet.AddRange(newLevelTypes.Except(oldLevelTypes));
                }

                var newLevels = item.Levels.ToArray();
                var toRemoveLevels = levelsSet.Where(l => l.TestId == item.Id).Except(newLevels);
                levelsSet.RemoveRange(toRemoveLevels);

                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<Test>().Find(id);
            if (item != null)
            {
                dbContext.Set<Test>().Remove(item);
            }
        }

        private IQueryable<Test> GetAllQuery()
        {
            return dbContext.Set<Test>()
                .Include(v => v.TestTemplate)
                .Include(v => v.Levels)
                .ThenInclude(l => l.Tasks)
                .ThenInclude(t => t.Task);
        }
    }
}
