using System.Threading.Tasks;
using LMS.Entries;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get;}
        Task SaveAsync();
    }
}
