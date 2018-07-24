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
        private readonly ITaskSource taskSource;

        public TestTemplateService(ITaskSource taskSource, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.taskSource = taskSource;
        }

        public TestTemplateDTO GetDefaultTemplate()
        {
            return new TestTemplateDTO
            {
                Levels =
                {
                    new TestTemplateLevelDTO()
                }
            };
        }

        public TestTemplateDTO GetById(int id)
        {
            var template = unitOfWork.TestTemplates.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TestTemplate>(id);
            }

            var templateDto = mapper.Map<TestTemplate, TestTemplateDTO>(template);

            foreach (var level in templateDto.Levels)
            {
                level.ValidTaskCount = taskSource.Filter(level.Filter).Count();
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
            if (!testTemplate.Levels.Any())
            {
                throw new ArgumentException("Template should contains at least one level");
            }
            if (!testTemplate.Levels.All(l => l.Filter.TaskTypeIds.Any()))
            {
                throw new ArgumentException("Every level should contains at least one task type");
            }
            if (!testTemplate.Levels.All(l => l.Filter.CategoryIds.Any()))
            {
                throw new ArgumentException("Every level should contains at least one category");
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
            if (!testTemplate.Levels.Any())
            {
                throw new ArgumentException("Template should contains at least one level");
            }
            if (!testTemplate.Levels.All(l => l.Filter.TaskTypeIds.Any()))
            {
                throw new ArgumentException("Every level should contains at least one task type");
            }
            if (!testTemplate.Levels.All(l => l.Filter.CategoryIds.Any()))
            {
                throw new ArgumentException("Every level should contains at least one category");
            }

            var createdTest = mapper.Map<TestTemplateDTO, TestTemplate>(testTemplate);

            unitOfWork.TestTemplates.Create(createdTest);

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TestTemplateSummary> GetTemplatesSummary()
        {
            var templates = unitOfWork.TestTemplates.GetAll().ToArray();
            return mapper
                .Map<IEnumerable<TestTemplate>, IEnumerable<TestTemplateSummary>>(templates)
                .Zip(templates, (m, t) => (dto: m, entity: t))
                .Select(tuple =>
                {
                    var (dto, entity) = tuple;
                    if (entity.Levels.Count == 0)
                    {
                        return dto;
                    }

                    dto.AvgComplexity = entity.Levels.Average(l => (l.MaxComplexity + l.MinComplexity) / 2);
                    dto.Levels = entity.Levels
                        .Select(level =>
                        {
                            var filter = mapper.Map<TestTemplateLevel, TestTemplateLevelDTO>(level).Filter;
                            var types = mapper
                                .Map<IEnumerable<LevelTaskType>, IEnumerable<TaskTypeDTO>>(level.TaskTypes)
                                .ToList();
                            return new TestTemplateSummaryLevel
                            {
                                CountPerTypes = types
                                    .Select(type =>
                                    {
                                        filter.TaskTypeIds = new List<int> { type.Id };
                                        var validCount = taskSource.Filter(filter).Count();
                                        return (type.Title, validCount);
                                    })
                                    .ToList()
                            };
                        })
                        .ToList();
                    return dto;
                });
        }
    }
}
