using LMS.Entities;
using LMS.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LMS.Data.Repositories
{
    public class TestSessionUserRepository : IRepository<TestSessionUser>
    {
        private readonly DbContext dbContext;

        public TestSessionUserRepository(DbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<TestSessionUser> GetAll()
        {
            return GetAllQuery()
                .ToList();
        }

        public IEnumerable<TestSessionUser> Filter(Expression<Func<TestSessionUser, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public TestSessionUser Get(int SessionId, string UserId)
        {
            return GetAllQuery()
                .FirstOrDefault(t => t.SessionId == SessionId && t.UserId == UserId);
        }

        public TestSessionUser Find(Expression<Func<TestSessionUser, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .FirstOrDefault();
        }

        public void Create(TestSessionUser item)
        {
            dbContext.Set<TestSessionUser>().Add(item);
        }

        public void Update(TestSessionUser item)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Set<TestSessionUser>().Update(item);
                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<TestSessionUser>().Find(id);
            if (item != null)
            {
                dbContext.Set<TestSessionUser>().Remove(item);
            }
        }

        private IQueryable<TestSessionUser> GetAllQuery()
        {
            return dbContext.Set<TestSessionUser>()
                .Include(t => t.Test)
                .ThenInclude(t=>t.Levels)
                .ThenInclude(m => m.Tasks)
                .ThenInclude(m=>m.Task)
                .ThenInclude(m=>m.AnswerOptions)

                .Include(t => t.Test)
                .ThenInclude(t => t.Levels)
                .ThenInclude(m => m.Tasks)
                .ThenInclude(m => m.Task)
                .ThenInclude(m=>m.Category)

                .Include(t => t.Test)
                .ThenInclude(t => t.Levels)
                .ThenInclude(m => m.Tasks)
                .ThenInclude(m => m.Task)
                .ThenInclude(m => m.Type)

                .Include(t => t.Answers);
        }

        public TestSessionUser Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
