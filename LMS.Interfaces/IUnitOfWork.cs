using System.Threading.Tasks;
using LMS.Entities;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get;}
        IRepository<Category> Categories { get; }

        Task SaveAsync();
    }
}
