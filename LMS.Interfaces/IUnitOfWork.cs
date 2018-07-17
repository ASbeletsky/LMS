using LMS.Entities;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Category> Categories { get; }
        IRepository<Task> Tasks { get; }
        IRepository<TaskType> TaskTypes { get; }
        IRepository<TestTemplate> TestConfigs { get; }
        IRepository<Level> Levels { get; }
        
        System.Threading.Tasks.Task SaveAsync();
    }
}
