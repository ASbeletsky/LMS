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
        }

        public IRepository<Category> Categories { get; }
        public IRepository<Task> Tasks { get; }
        public IRepository<TaskType> TaskTypes { get; }

        public System.Threading.Tasks.Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
