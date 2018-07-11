using LMS.Interfaces;
using LMS.Data.Models;
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
            ProblemTypes = new BasicRepository<ProblemType>(context);
            TestCategories = new BasicRepository<TestCategory>(context);
            Choices = new BasicRepository<Choice>(context);
            Problems = new TestProblemRepository(context);
            Tests = new TestRepository(context);
        }

        public IRepository<Choice> Choices { get; }
        public IRepository<ProblemType> ProblemTypes { get; }
        public IRepository<TestProblem> Problems { get; }
        public IRepository<TestCategory> TestCategories { get; }
        public IRepository<Test> Tests { get; }

        public Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
