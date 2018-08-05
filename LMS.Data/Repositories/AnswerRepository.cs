using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Data.Repositories
{
    public class AnswerRepository : IRepository<TaskAnswer>
    {
        private readonly DbContext dbContext;

        public AnswerRepository(DbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<TaskAnswer> GetAll()
        {
            return GetAllQuery()
                .ToList();
        }

        public IEnumerable<TaskAnswer> Filter(Expression<Func<TaskAnswer, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public TaskAnswer Get(int id)
        {
            return GetAllQuery()
                .FirstOrDefault(t => t.Id == id);
        }

        public TaskAnswer Find(Expression<Func<TaskAnswer, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .FirstOrDefault();
        }

        public void Create(TaskAnswer item)
        {
            dbContext.Set<TaskAnswer>().Add(item);
        }

        public void Update(TaskAnswer item)
        {
            dbContext.Set<TaskAnswer>().Update(item);
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<TaskAnswer>().Find(id);
            if (item != null)
            {
                dbContext.Set<TaskAnswer>().Remove(item);
            }
        }

        private IQueryable<TaskAnswer> GetAllQuery()
        {
            return dbContext.Set<TaskAnswer>()
                .Include(a => a.TestSessionUser);
        }
    }
}
