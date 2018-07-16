using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;

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
             return set.AddAsync(item);
        }

        public Task DeleteAsync(int id)
        {
           var item = set.Find(id);
            if (item != null)
            {
                set.Remove(item);
            }
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T item)
        {
            set.Update(item);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> Filter(Func<T, bool> predicate)
        {
            return await set.ToAsyncEnumerable().Where(predicate).ToArray();
        }

        public async Task<T> FindAsync(Func<T, bool> predicate)
        {
            return await set.FirstOrDefaultAsync(t => predicate(t), CancellationToken.None);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await set.ToAsyncEnumerable().ToArray();
        }

        public Task<T> GetAsync(int id)
        {
            return set.FindAsync(id);
        }

        

      


        
    }
}
