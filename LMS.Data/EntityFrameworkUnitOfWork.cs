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
            QuestionTypes = new BasicRepository<QuestionType>(context);
            Categories = new BasicRepository<Category>(context);
            Questions = new QuestionRepository(context);
        }

        public IRepository<Category> Categories { get; }
        public IRepository<Question> Questions { get; }
        public IRepository<QuestionType> QuestionTypes { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
