using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TaskTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TaskTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<TaskTypeDTO> GetAll()
        {
            return mapper.Map<IEnumerable<TaskType>, IEnumerable<TaskTypeDTO>>(
                unitOfWork.TaskTypes.GetAll());
        }
    }
}
