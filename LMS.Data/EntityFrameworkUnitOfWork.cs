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
            TaskTypes = new BasicRepository<TaskType>(context);
            Categories = new BasicRepository<Category>(context);
            Tasks = new TaskRepository(context);
            TestConfigs = new BasicRepository<TestTemplate>(context);
            Levels = new LevelRepository(context);
        }

        public IRepository<Category> Categories { get; }
        public IRepository<Task> Tasks { get; }
        public IRepository<TaskType> TaskTypes { get; }
        public IRepository<TestTemplate> TestConfigs { get; }
        public IRepository<Level> Levels { get; }

        public System.Threading.Tasks.Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
