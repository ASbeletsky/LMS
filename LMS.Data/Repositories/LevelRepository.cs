using System;
using System.Collections.Generic;
using LMS.Entities;
using LMS.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories
{
    public class LevelRepository : IRepository<Level>
    {
        private readonly DbSet<Task> set;

        public LevelRepository(LMSDbContext context)
        {
            set = context.Set<Task>();
        }
        
        public IEnumerable<Level> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Level> Filter(Func<Level, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Level Get(int id)
        {
            throw new NotImplementedException();
        }

        public Level Find(Func<Level, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Level item)
        {
            throw new NotImplementedException();
        }

        public void Update(Level item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
