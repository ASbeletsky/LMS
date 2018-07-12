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

            QuestionCategories = new BasicRepository<QuestionCategory>(context);
        }

        public IRepository<QuestionCategory> QuestionCategories { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
