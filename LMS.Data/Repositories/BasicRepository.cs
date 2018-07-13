using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Interfaces;

namespace LMS.Data.Repositories
{
    public class BasicRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbSet<T> set;

        public BasicRepository(LMSDbContext context)
        {
            set = context.Set<T>();
        }

        public void CreateAsync(T item)
        {
            set.Add(item);
        }

        public void DeleteAsync(int id)
        {
            var item = set.Find(id);
            if (item != null)
            {
                set.Remove(item);
            }
        }

        public IEnumerable<T> Filter(Func<T, bool> predicate)
        {
            return set.Where(predicate);
        }

        public T Find(Func<T, bool> predicate)
        {
            return set.FirstOrDefault(predicate);
        }

        public T Get(int id)
        {
            return set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return set;
        }

        public void UpdateAsync(T item)
        {
            set.Update(item);
        }
    }
}
