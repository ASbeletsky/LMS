using System;
using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TestTemplateService : BaseService
    {
        private readonly TaskService taskService;

        public TestTemplateService(TaskService taskService, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.taskService = taskService;
        }

        public TestTemplateDTO GetById(int id)
        {
            var template = unitOfWork.TestTemplates.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TestTemplateDTO>(id);
            }

            var templateDto = mapper.Map<TestTemplate, TestTemplateDTO>(template);

            foreach (var level in templateDto.Levels)
            {
                level.ValidTaskCount = taskService.GetByFilter(level.Filter).Count();
            }

            return templateDto;
        }

        public Task DeleteByIdAsync(int id)
        {
            unitOfWork.TestTemplates.Delete(id);
            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TestTemplateDTO testTemplate)
        {
            if (testTemplate == null)
            {
                throw new ArgumentNullException(nameof(testTemplate));
            }

            var updatedTest = mapper.Map<TestTemplateDTO, TestTemplate>(testTemplate);

            unitOfWork.TestTemplates.Update(updatedTest);

            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(TestTemplateDTO testTemplate)
        {
            if (testTemplate == null)
            {
                throw new ArgumentNullException(nameof(testTemplate));
            }

            var updatedTest = mapper.Map<TestTemplateDTO, TestTemplate>(testTemplate);

            unitOfWork.TestTemplates.Create(updatedTest);

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TestTemplateListItemDTO> GetListItems()
        {
            var templates = unitOfWork.TestTemplates.GetAll().ToArray();
            return mapper
                .Map<IEnumerable<TestTemplate>, IEnumerable<TestTemplateListItemDTO>>(templates)
                .Zip(templates, (m, t) => (dto: m, entity: t))
                .Select(tuple =>
                {
                    var (dto, entity) = tuple;
                    dto.AvgComplexity = entity.Levels.Average(l => (l.MaxComplexity + l.MinComplexity) / 2);
                    dto.Tasks = entity.Levels
                        .Select(level =>
                        {
                            var filter = mapper.Map<TestTemplateLevel, TestTemplateLevelDTO>(level).Filter;
                            var types = mapper
                                .Map<IEnumerable<LevelTaskType>, IEnumerable<TaskTypeDTO>>(level.TaskTypes).ToList();
                            return new TaskTemplateDTO
                            {
                                Types = types,
                                ValidTaskCount = taskService.GetByFilter(filter).Count()
                            };
                        })
                        .ToList();
                    return dto;
                });
        }
    }
}
