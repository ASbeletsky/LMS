using System.Threading.Tasks;
using LMS.Entities;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Category> Categories { get; }

        Task SaveAsync();
    }
}
