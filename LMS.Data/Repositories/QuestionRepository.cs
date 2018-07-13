using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Data.Repositories
{
    public class QuestionRepository : IRepository<Question>
    {
        private readonly DbSet<Question> set;

        public QuestionRepository(LMSDbContext context)
        {
            set = context.Set<Question>();
        }

        public void Create(Question item)
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

        public IEnumerable<Question> Filter(Func<Question, bool> predicate)
        {
            return set
                .Include(q => q.Category)
                .Include(q => q.Type)
                .Where(predicate);
        }

        public Question Find(Func<Question, bool> predicate)
        {
            return set
                .Include(q => q.Category)
                .Include(q => q.Type)
                .FirstOrDefault(predicate);
        }

        public Question Get(int id)
        {
            return set
                .Include(q => q.Category)
                .Include(q => q.Type)
                .FirstOrDefault(q => q.Id == id);
        }

        public IEnumerable<Question> GetAll()
        {
            return set
                .Include(q => q.Category)
                .Include(q => q.Type);
        }

        public void Update(Question item)
        {
            set.Update(item);
        }
    }
}
