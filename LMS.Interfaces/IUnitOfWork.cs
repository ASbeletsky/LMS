using System.Threading.Tasks;
using LMS.Entities;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<QuestionCategory> QuestionCategories { get; }

        Task SaveAsync();
    }
}
