using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TestConfigService : BaseService
    {
        public TestConfigService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }
        
        
    }
}
