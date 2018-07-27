using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using LMS.Entities;
using LMS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories
{
    public class TestSessionRepository : IRepository<TestSession>
    {
        private readonly DbContext dbContext;

        public TestSessionRepository(DbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<TestSession> GetAll()
        {
            return GetAllQuery()
                .ToList();
        }

        public IEnumerable<TestSession> Filter(Expression<Func<TestSession, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public TestSession Get(int id)
        {
            return GetAllQuery()
                .FirstOrDefault(t => t.Id == id);
        }

        public TestSession Find(Expression<Func<TestSession, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .FirstOrDefault();
        }

        public void Create(TestSession item)
        {
            dbContext.Set<TestSession>().Add(item);
        }

        public void Update(TestSession item)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var exameneesSet = dbContext.Set<TestSessionUser>();
                var testsSet = dbContext.Set<TestSessionTest>();

                var entry = dbContext.Set<TestSession>().Update(item);

                var newExamenees = item.Members.ToArray();
                var toRemoveExamenees = exameneesSet
                    .Where(l => l.SessionId == item.Id)
                    .Except(newExamenees);
                exameneesSet.RemoveRange(toRemoveExamenees);

                var newTests = item.Tests.ToArray();
                var toRemoveTests = testsSet
                    .Where(l => l.SessionId == item.Id)
                    .Except(newTests);
                testsSet.RemoveRange(toRemoveTests);

                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<TestSession>().Find(id);
            if (item != null)
            {
                dbContext.Set<TestSession>().Remove(item);
            }
        }

        private IQueryable<TestSession> GetAllQuery()
        {
            return dbContext.Set<TestSession>()
                .Include(v => v.Members)
                .Include(v => v.Tests)
                .ThenInclude(l => l.Test);
        }
    }
}
