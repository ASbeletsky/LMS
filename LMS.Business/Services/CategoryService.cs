using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;
using System.Linq;
using System;
using Task = System.Threading.Tasks.Task;

namespace LMS.Business.Services
{
    public class CategoryService : BaseService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public Task DeleteAsync(int categoryId)
        {
            unitOfWork.Categories.Delete(categoryId);

            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(CategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (!string.IsNullOrEmpty(category.Title))
            {
                var entry = mapper.Map<CategoryDTO, Entities.Category>(category);

                unitOfWork.Categories.Create(entry);
            }

            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryDTO));
            }
            if (string.IsNullOrEmpty(categoryDTO.Title))
            {
                throw new ArgumentNullException(nameof(categoryDTO.Title));
            }

            var category = mapper.Map<CategoryDTO, Category>(categoryDTO);
            unitOfWork.Categories.Update(category);

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = unitOfWork.Categories.GetAll();
            var categoriesDTO = mapper.Map<IEnumerable<Entities.Category>, IEnumerable<CategoryDTO>>(categories);

            foreach (var category in categoriesDTO)
            {
                category.TasksCount = unitOfWork.Tasks.Filter(b => b.CategoryId == category.Id).Count();
                if (category.ParentCategoryId != null)
                    category.ParentCategory = mapper.Map<Category, CategoryDTO>(categories.Where(c => c.Id == category.ParentCategoryId).First());
            }

            return categoriesDTO;
        }

        public CategoryDTO GetById(int categoryId)
        {
            var category = unitOfWork.Categories.Get(categoryId);
            if (category == null)
            {
                throw new EntityNotFoundException<Category>(categoryId);
            }

            return mapper.Map<Category, CategoryDTO>(category);
        }

        public IEnumerable<CategoryDTO> GetAvailableCategories()
        {
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(
                unitOfWork.Categories.Filter(t => t.ParentCategoryId == null));
        }

        public IEnumerable<CategoryDTO> GetAvailableCategories(int id)
        {
            return (mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(
                unitOfWork.Categories.Filter(t => t.ParentCategoryId == null && t.Id != id)));
        }

        public int GetAmountChildrenCategories(int id)
        {
            return unitOfWork.Categories.Filter(t => t.ParentCategoryId == id).Count();
        }
    }
}
