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
            Tests = new TestRepository(context);
            TestSessions = new TestSessionRepository(context);
            Examinee = new ExamineeRepositoty(context);
            Answers = new AnswerRepository(context);
            TestSessionUsers = new TestSessionUserRepository(context);
            UserRepository = usersRepo;
        }

        public IRepository<Category> Categories { get; }
        public IRepository<Task> Tasks { get; }
        public IRepository<TaskType> TaskTypes { get; }
        public IRepository<TestTemplate> TestTemplates { get; }
        public IRepository<Test> Tests { get; }
        public IRepository<TestSession> TestSessions { get; }
        public IRepositoryAsync<User> UserRepository { get; }
        public IRepository<Examinee> Examinee { get; }
        public IRepository<TestSessionUser> TestSessionUsers { get; }
        public IRepository<TaskAnswer> Answers { get; }

        public System.Threading.Tasks.Task SaveAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
