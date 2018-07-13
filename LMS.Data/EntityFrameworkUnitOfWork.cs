using System.Threading.Tasks;
using LMS.Data.Repositories;
using LMS.Interfaces;
using LMS.Entries;

namespace LMS.Data
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly LMSDbContext dbContext;

        public EntityFrameworkUnitOfWork(LMSDbContext context)
        {
            dbContext = context;
            UserRepository = new UserRepository(context);
        }

        public IRepository<User> UserRepository { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
