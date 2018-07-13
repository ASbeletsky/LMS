using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class CategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(
                unitOfWork.Categories.GetAll());
        }
    }
}
