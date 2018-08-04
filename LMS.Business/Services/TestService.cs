using System;
using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TestService : BaseService
    {
        private readonly ITaskSource taskSource;

        public TestService(ITaskSource taskSource, IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.taskSource = taskSource;
        }

        public TestDTO GetById(int id)
        {
            var test = unitOfWork.Tests.Get(id);
            if (test == null)
            {
                throw new EntityNotFoundException<Test>(id);
            }

            return mapper.Map<Test, TestDTO>(test);
        }

        public Task DeleteByIdAsync(int id)
        {
            unitOfWork.Tests.Delete(id);
            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TestDTO test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            if (!test.Levels.Any())
            {
                throw new ArgumentException("Test should contains at least one level");
            }

            test.Levels = test.Levels.Where(l => !l.TemplateDeleted).ToList();

            if (!test.Levels.All(l => l.Tasks.Any()))
            {
                throw new ArgumentException("Every level should contains at least one task");
            }

            var updatedTest = mapper.Map<TestDTO, Test>(test);

            unitOfWork.Tests.Update(updatedTest);

            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(TestDTO test)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }
            if (!test.Levels.Any())
            {
                throw new ArgumentException("Test should contains at least one level");
            }
            if (!test.Levels.All(l => l.Tasks.Any()))
            {
                throw new ArgumentException("Every level should contains at least one task");
            }

            var createdTest = mapper.Map<TestDTO, Test>(test);

            unitOfWork.Tests.Create(createdTest);

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TestDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Test>, IEnumerable<TestDTO>>(
                unitOfWork.Tests.GetAll())
                .Select(v => { BindToTemplate(v, v.TestTemplateId); return v; });
        }

        public IEnumerable<TestDTO> FilterByTemplate(int templateId)
        {
            return mapper.Map<IEnumerable<Test>, IEnumerable<TestDTO>>(
                unitOfWork.Tests.Filter(t => t.TestTemplateId == templateId));
        }

        public void BindToTemplate(TestDTO test, int? testTemplateId)
        {
            if (test == null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            if (!testTemplateId.HasValue)
            {
                foreach (var levelToRemove in test.Levels)
                {
                    levelToRemove.TemplateDeleted = true;
                }
                return;
            }

            var template = unitOfWork.TestTemplates.Get(testTemplateId.Value);
            if (template == null)
            {
                throw new EntityNotFoundException<TestTemplateDTO>();
            }

            test.TestTemplateId = testTemplateId;
            if (string.IsNullOrEmpty(test.Title))
            {
                var prevTestsCount = unitOfWork.Tests
                    .Filter(v => v.TestTemplateId == testTemplateId)
                    .Count();
                test.Title = "Test #" + (prevTestsCount + 1);
            }
            foreach (var templateLevel in template.Levels)
            {
                var templateLevelDTO = mapper.Map<TestTemplateLevel, TestTemplateLevelDTO>(templateLevel);
                var availableTasks = taskSource.Filter(templateLevelDTO.Filter);

                var existedLevel = test.Levels
                    .FirstOrDefault(l => l.TestTemplateLevelId == templateLevel.Id);

                if (existedLevel == null)
                {
                    test.Levels.Add(new TestLevelDTO()
                    {
                        Description = templateLevel.Description,
                        TestTemplateLevelId = templateLevel.Id,
                        RequiredTasksCount = templateLevel.TasksCount,
                        TestId = test.Id,
                        AvailableTasks = availableTasks.ToList()
                    });
                }
                else
                {
                    existedLevel.RequiredTasksCount = templateLevel.TasksCount;
                    existedLevel.AvailableTasks = availableTasks.ToList();
                    existedLevel.TemplateModified = existedLevel.Tasks
                        .Select(t => t.Id)
                        .Except(availableTasks.Select(t => t.Id))
                        .Any();
                }
            }

            var levelsToRemove = test.Levels
                .Where(l => template.Levels.All(tl => tl.Id != l.TestTemplateLevelId));
            foreach (var levelToRemove in levelsToRemove)
            {
                levelToRemove.TemplateDeleted = true;
            }
        }
    }
}
