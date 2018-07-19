using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class CategoryService : BaseService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(
                unitOfWork.Categories.GetAll());
        }
    }
}
