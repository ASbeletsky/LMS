using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LMS.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}
