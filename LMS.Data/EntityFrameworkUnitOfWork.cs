using System.Threading.Tasks;
using LMS.Entities;
using LMS.Interfaces;
using LMS.Data.Repositories;

namespace LMS.Data
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly LMSDbContext dbContext;

        public EntityFrameworkUnitOfWork(LMSDbContext context)
        {
            dbContext = context;

            Categories = new BasicRepository<Category>(context);
            UserRepository = new UserRepository(context);
        }

        public IRepository<Category> Categories { get; }

        public IRepository<User> UserRepository { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
