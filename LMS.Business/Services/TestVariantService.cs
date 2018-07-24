using System;
using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TestVariantService : BaseService
    {
        private readonly ITaskSource taskSource;

        public TestVariantService(ITaskSource taskSource, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.taskSource = taskSource;
        }

        public TestVariantDTO GetById(int id)
        {
            var variant = unitOfWork.TestVariants.Get(id);
            if (variant == null)
            {
                throw new EntityNotFoundException<TestVariant>(id);
            }

            return mapper.Map<TestVariant, TestVariantDTO>(variant);
        }

        public Task DeleteByIdAsync(int id)
        {
            unitOfWork.TestVariants.Delete(id);
            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TestVariantDTO testVariant)
        {
            if (testVariant == null)
            {
                throw new ArgumentNullException(nameof(testVariant));
            }
            if (!testVariant.Levels.Any())
            {
                throw new ArgumentException("Variant should contains at least one level");
            }
            if (!testVariant.Levels.All(l => l.Tasks.Any()))
            {
                throw new ArgumentException("Every level should contains at least one task");
            }

            var updatedTest = mapper.Map<TestVariantDTO, TestVariant>(testVariant);

            unitOfWork.TestVariants.Update(updatedTest);

            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(TestVariantDTO testVariant)
        {
            if (testVariant == null)
            {
                throw new ArgumentNullException(nameof(testVariant));
            }
            if (!testVariant.Levels.Any())
            {
                throw new ArgumentException("Variant should contains at least one level");
            }
            if (!testVariant.Levels.All(l => l.Tasks.Any()))
            {
                throw new ArgumentException("Every level should contains at least one task");
            }

            var createdTest = mapper.Map<TestVariantDTO, TestVariant>(testVariant);

            unitOfWork.TestVariants.Create(createdTest);

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TestVariantDTO> GetAll()
        {
            return mapper.Map<IEnumerable<TestVariant>, IEnumerable<TestVariantDTO>>(
                unitOfWork.TestVariants.GetAll());
        }

        public void BindToTemplate(TestVariantDTO testVariant, int testTemplateId)
        {
            if (testVariant == null)
            {
                throw new ArgumentNullException(nameof(testVariant));
            }

            var template = unitOfWork.TestTemplates.Get(testTemplateId);
            if (template == null)
            {
                throw new EntityNotFoundException<TestTemplateDTO>();
            }

            testVariant.TestTemplateId = testTemplateId;
            if (string.IsNullOrEmpty(testVariant.Title))
            {
                var prevVariantsCount = unitOfWork.TestVariants
                    .Filter(v => v.TestTemplateId == testTemplateId)
                    .Count();
                testVariant.Title = "Variant #" + (prevVariantsCount + 1);
            }
            foreach (var templateLevel in template.Levels)
            {
                var templateLevelDTO = mapper.Map<TestTemplateLevel, TestTemplateLevelDTO>(templateLevel);
                var availableTasks = taskSource.Filter(templateLevelDTO.Filter);

                var existedLevel = testVariant.Levels
                    .FirstOrDefault(l => l.TestTemplateLevelId == templateLevel.Id);

                if (existedLevel == null)
                {
                    testVariant.Levels.Add(new TestVariantLevelDTO()
                    {
                        Description = templateLevel.Description,
                        TestTemplateLevelId = templateLevel.Id,
                        TestVariantId = testVariant.Id,
                        Filter = templateLevelDTO.Filter,
                        AvailableTasks = availableTasks.ToList()
                    });
                }
                else
                {
                    existedLevel.Filter = templateLevelDTO.Filter;
                    existedLevel.AvailableTasks = availableTasks.ToList();
                    existedLevel.TemplateModified = existedLevel.Tasks
                        .Select(t => t.Id)
                        .Except(availableTasks.Select(t => t.Id))
                        .Any();
                }
            }

            var levelsToRemove = testVariant.Levels
                .Where(l => template.Levels.All(tl => tl.Id != l.TestTemplateLevelId));
            foreach (var levelToRemove in levelsToRemove)
            {
                levelToRemove.Filter = null;
                levelToRemove.TemplateDeleted = true;
            }
        }
    }
}
