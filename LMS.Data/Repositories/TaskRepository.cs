using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Entities;
using LMS.Interfaces;
using System.Linq.Expressions;

namespace LMS.Data.Repositories
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly DbSet<Task> set;

        public TaskRepository(LMSDbContext context)
        {
            set = context.Set<Task>();
        }

        public void Create(Task item)
        {
            set.Add(item);
        }

        public void Delete(int id)
        {
            var item = set.Find(id);
            if (item != null)
            {
                set.Remove(item);
            }
        }

        public IEnumerable<Task> Filter(Expression<Func<Task, bool>> predicate)
        {
            return set
                .Include(t => t.Category)
                .Include(t => t.Type)
                .Include(t => t.AnswerOptions)
                .Where(predicate);
        }

        public Task Find(Expression<Func<Task, bool>> predicate)
        {
            return set
                .Include(t => t.Category)
                .Include(t => t.Type)
                .Include(t => t.PreviousVersion)
                .Include(t => t.AnswerOptions)
                .FirstOrDefault(predicate);
        }

        public Task Get(int id)
        {
            return set
                .Include(t => t.Category)
                .Include(t => t.Type)
                .Include(t => t.PreviousVersion)
                .Include(t => t.AnswerOptions)
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Task> GetAll()
        {
            return set
                .Include(t => t.Category)
                .Include(t => t.Type)
                .Include(t => t.AnswerOptions);
        }

        public void Update(Task item)
        {
            set.Update(item);
        }
    }
}
