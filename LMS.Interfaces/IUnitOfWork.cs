using System.Threading.Tasks;
namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {

       // IRepository<User> UserRepository;
        Task SaveAsync();
    }
}
