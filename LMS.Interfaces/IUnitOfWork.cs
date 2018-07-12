using System.Threading.Tasks;
using LMS.Entities;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Choice> Choices { get; }
        IRepository<QuestionType> QuestionTypes { get; }
        IRepository<Question> Questions { get; }
        IRepository<TestCategory> TestCategories { get; }
        IRepository<Test> Tests { get; }

        Task SaveAsync();
    }
}
