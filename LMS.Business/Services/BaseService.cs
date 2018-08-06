using LMS.Interfaces;

namespace LMS.Business.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
