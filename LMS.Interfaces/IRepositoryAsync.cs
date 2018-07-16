using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Filter(Func<T, bool> predicate);
        Task<T> GetAsync(int id);
        Task<T> FindAsync(Func<T, bool> predicate);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}