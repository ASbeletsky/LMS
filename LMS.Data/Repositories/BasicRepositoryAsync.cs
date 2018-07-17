using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Interfaces;

namespace LMS.Data.Repositories
{
    public class BasicRepositoryAsync<T> : IRepositoryAsync<T>
        where T : class
    {
        private readonly DbSet<T> set;

        public BasicRepositoryAsync(LMSDbContext context)
        {
            set = context.Set<T>();
        }

        public Task CreateAsync(T item)
        {
            set.Add(item);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await set.FindAsync(id);
            if (item != null)
            {
                set.Remove(item);
            }
        }

        public Task UpdateAsync(T item)
        {
            set.Update(item);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate)
        {
            return await set.Where(predicate).ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await set.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await set.ToListAsync();
        }

        public Task<T> GetAsync(int id)
        {
            return set.FindAsync(id);
        }
    }
}
