using System.Threading.Tasks;
using LMS.Entities;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Choice> Choices { get; }
        IRepository<ProblemType> ProblemTypes { get; }
        IRepository<TestProblem> Problems { get; }
        IRepository<TestCategory> TestCategories { get; }
        IRepository<Test> Tests { get; }

        Task SaveAsync();
    }
}
