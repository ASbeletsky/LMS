using LMS.Entities;
using LMS.Interfaces;
using LMS.Data.Repositories;
using System.Threading.Tasks;

namespace LMS.Data
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly LMSDbContext dbContext;
        public EntityFrameworkUnitOfWork(LMSDbContext context)
        {
            dbContext = context;
            QuestionTypes = new BasicRepository<QuestionType>(context);
            TestCategories = new BasicRepository<TestCategory>(context);
            Choices = new BasicRepository<Choice>(context);
            Questions = new QuestionRepository(context);
            Tests = new TestRepository(context);
        }

        public IRepository<Choice> Choices { get; }
        public IRepository<QuestionType> QuestionTypes { get; }
        public IRepository<Question> Questions { get; }
        public IRepository<TestCategory> TestCategories { get; }
        public IRepository<Test> Tests { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
