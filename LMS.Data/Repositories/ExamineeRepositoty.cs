using LMS.Entities;
using LMS.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LMS.Data.Repositories
{
    public class ExamineeRepositoty : IRepository<Examinee>
    {
        private readonly DbContext dbContext;

        public ExamineeRepositoty(DbContext context)
        {
            dbContext = context;
        }

        public IEnumerable<Examinee> GetAll()
        {
            return GetAllQuery()
                .ToList();
        }

        public IEnumerable<Examinee> Filter(Expression<Func<Examinee, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .ToList();
        }

        public Examinee Get(int id)
        {
            return GetAllQuery()
                .FirstOrDefault(t => t.Id == id);
        }

        public Examinee Find(Expression<Func<Examinee, bool>> predicate)
        {
            return GetAllQuery()
                .Where(predicate)
                .FirstOrDefault();
        }

        public void Create(Examinee item)
        {
            dbContext.Set<Examinee>().Add(item);
        }

        public void Update(Examinee item)
        {
            dbContext.Update(item);
        }

        public void Delete(int id)
        {
            var item = dbContext.Set<Examinee>().Find(id);
            if (item != null)
            {
                dbContext.Set<Examinee>().Remove(item);
            }
        }

        private IQueryable<Examinee> GetAllQuery()
        {
            return dbContext.Set<Examinee>();
        }
    }
}
