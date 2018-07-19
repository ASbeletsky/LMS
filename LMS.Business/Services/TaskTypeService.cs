using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TaskTypeService : BaseService
    {
        public TaskTypeService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<TaskTypeDTO> GetAll()
        {
            return mapper.Map<IEnumerable<TaskType>, IEnumerable<TaskTypeDTO>>(
                unitOfWork.TaskTypes.GetAll());
        }
    }
}
