using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class QuestionTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public QuestionTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<QuestionTypeDTO> GetAll()
        {
            return mapper.Map<IEnumerable<QuestionType>, IEnumerable<QuestionTypeDTO>>(
                unitOfWork.QuestionTypes.GetAll());
        }
    }
}
