using LMS.Entities;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryAsync<User> UserRepository { get;}
        IRepository<Category> Categories { get; }
        IRepository<Task> Tasks { get; }
        IRepository<TaskType> TaskTypes { get; }
        IRepository<TestTemplate> TestTemplates { get; }
        IRepository<TestVariant> TestVariants { get; }

        System.Threading.Tasks.Task SaveAsync();
    }
}
