using System;
using System.Collections.Generic;

namespace LMS.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Filter(Func<T, Boolean> predicate);
        T Get(int id);
        T Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
