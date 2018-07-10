using System.Threading.Tasks;

namespace LMS.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
