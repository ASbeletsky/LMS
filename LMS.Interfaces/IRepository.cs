using System;
using System.Collections.Generic;

namespace LMS.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Filter(Func<T, bool> predicate);
        T Get(int id);
        T Find(Func<T, bool> predicate);
        void CreateAsync(T item);
        void UpdateAsync(T item);
        void DeleteAsync(int id);
    }
}
