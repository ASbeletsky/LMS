using LMS.Entities;
using LMS.Interfaces;
using LMS.Data.Repositories;

namespace LMS.Data
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly LMSDbContext dbContext;

        public EntityFrameworkUnitOfWork(LMSDbContext context, IRepositoryAsync<User> usersRepo)
        {
            dbContext = context;
            TaskTypes = new BasicRepository<TaskType>(context);
            Categories = new BasicRepository<Category>(context);
            Tasks = new TaskRepository(context);
            TestTemplates = new TestTemplateRepository(context);
            UserRepository = usersRepo;
        }

        public IRepository<Category> Categories { get; }
        public IRepository<Task> Tasks { get; }
        public IRepository<TaskType> TaskTypes { get; }
        public IRepository<TestTemplate> TestTemplates { get; }

        public IRepositoryAsync<User> UserRepository { get; }

        public IRepository<Answers> Answers { get; }

        public IRepository<Examenee> Examenees { get; }

        public System.Threading.Tasks.Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
