using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;
using LMS.Business.Services;
using Moq;
using Xunit;

namespace LMS.Test.Services
{
    public class TestTemplateServiceTests
    {
        private readonly Bootstrap.Mapping.AutoMapper mapper = new Bootstrap.Mapping.AutoMapper();

        [Fact]
        public void Should_Get_Item()
        {
            var templateGet = new TestTemplate
            {
                Id = 1,
                Levels =
                {
                    new TestTemplateLevel
                    {
                        TaskTypes =
                        {
                            new LevelTaskType
                            {
                                TaskTypeId = 5
                            }
                        }
                    }
                }
            };

            var repositoryMock = new Mock<IRepository<TestTemplate>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(templateGet);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => repositoryMock.Object);

            var taskServiceMock = new Mock<ITaskSource>();
            taskServiceMock.Setup(m => m.Filter(It.IsAny<TaskFilterDTO>())).Returns(new TaskClientDTO[3]);

            var service = new TestTemplateService(taskServiceMock.Object, unitOfWorkMock.Object, mapper);

            var actualGet = service.GetById(1);
            Assert.NotNull(actualGet);
            Assert.Single(actualGet.Levels);
            Assert.Equal(3, actualGet.Levels[0].ValidTaskCount);
            Assert.Single(actualGet.Levels[0].Filter.TaskTypeIds);
            Assert.Equal(5, actualGet.Levels[0].Filter.TaskTypeIds.Single());
            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Throw_NotFound_On_Get()
        {
            var repositoryMock = new Mock<IRepository<TestTemplate>>();
            repositoryMock.Setup(u => u.Get(1)).Returns<TestTemplate>(null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => repositoryMock.Object);

            var service = new TestTemplateService(null, unitOfWorkMock.Object, mapper);

            Assert.Throws<EntityNotFoundException<TestTemplate>>(() => service.GetById(1));

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Delete()
        {
            var repositoryMock = new Mock<IRepository<TestTemplate>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => repositoryMock.Object);

            var service = new TestTemplateService(null, unitOfWorkMock.Object, mapper);

            await service.DeleteByIdAsync(1);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Delete(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Update()
        {
            var singleLevelTypeId = 5;
            var singleCategoryId = 2;
            var templateUpdate = new TestTemplateDTO
            {
                Id = 1,
                Title = "Sample",
                Levels =
                {
                    new TestTemplateLevelDTO
                    {
                        Description = "Level desc",
                        Filter = new TaskFilterDTO
                        {
                            TaskTypeIds = { singleLevelTypeId },
                            CategoryIds = { singleCategoryId }
                        }
                    }
                }
            };

            var repositoryMock = new Mock<IRepository<TestTemplate>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => repositoryMock.Object);

            var service = new TestTemplateService(null, unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(templateUpdate);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Update(It.Is<TestTemplate>(t =>
                t.Id == templateUpdate.Id
                && t.Title == templateUpdate.Title
                && t.Levels.Single().Description == templateUpdate.Levels.Single().Description
                && t.Levels.Single().TaskTypes.Single().TaskTypeId == singleLevelTypeId
                && t.Levels.Single().Categories.Single().CategoryId == singleCategoryId)));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Create()
        {
            var singleLevelTypeId = 5;
            var singleCategoryId = 2;
            var templateCreate = new TestTemplateDTO
            {
                Id = 1,
                Title = "Sample",
                Levels =
                {
                    new TestTemplateLevelDTO
                    {
                        Description = "Level desc",
                        Filter = new TaskFilterDTO
                        {
                            TaskTypeIds = { singleLevelTypeId },
                            CategoryIds = { singleCategoryId }
                        }
                    }
                }
            };
            var repositoryMock = new Mock<IRepository<TestTemplate>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => repositoryMock.Object);

            var service = new TestTemplateService(null, unitOfWorkMock.Object, mapper);

            await service.CreateAsync(templateCreate);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Create(It.Is<TestTemplate>(t =>
                t.Id == templateCreate.Id
                && t.Title == templateCreate.Title
                && t.Levels.Single().Description == templateCreate.Levels.Single().Description
                && t.Levels.Single().Categories.Single().CategoryId == singleCategoryId
                && t.Levels.Single().TaskTypes.Single().TaskTypeId == singleLevelTypeId)));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Get_All_Active()
        {
            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.GetAll()).Returns(() => new[]
            {
                new TestTemplate
                {
                    Id = 1,
                    Title = "1",
                    Levels = new List<TestTemplateLevel>()
                },
                new TestTemplate
                {
                    Id = 2,
                    Title = "2",
                    Levels = new List<TestTemplateLevel>
                    {
                        new TestTemplateLevel
                        {
                            MaxComplexity = 3,
                            TaskTypes =
                            {
                                new LevelTaskType
                                {
                                    TaskType = new TaskType()
                                }
                            }
                        }
                    }
                },
                new TestTemplate
                {
                    Id = 3,
                    Title = "3",
                    Levels = new List<TestTemplateLevel>
                    {
                        new TestTemplateLevel
                        {
                            MinComplexity = 5,
                            TaskTypes =
                            {
                                new LevelTaskType
                                {
                                    TaskType = new TaskType()
                                }
                            }
                        }
                    }
                }
            });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var taskServiceMock = new Mock<ITaskSource>();
            taskServiceMock.Setup(m => m.Filter(It.Is<TaskFilterDTO>(l => l.MinComplexity == 5 && l.TaskTypeIds.Any())))
                .Returns(new TaskClientDTO[3]);
            taskServiceMock.Setup(m => m.Filter(It.Is<TaskFilterDTO>(l => l.MaxComplexity == 3 && l.TaskTypeIds.Any())))
                .Returns(new TaskClientDTO[2]);

            var service = new TestTemplateService(taskServiceMock.Object, unitOfWorkMock.Object, mapper);
            var testTemplateListItems = service.GetTemplatesSummary().ToArray();
            Assert.Equal(3, testTemplateListItems.GroupBy(t => t.Id).Count());
            Assert.True(testTemplateListItems.All(t => t.Id.ToString() == t.Title));
            Assert.Empty(testTemplateListItems[0].Levels);
            Assert.Equal(2, testTemplateListItems[1].Levels.Single().CountPerTypes.Sum(t => t.Count));
            Assert.Equal(3, testTemplateListItems[2].Levels.Single().CountPerTypes.Sum(t => t.Count));
        }
    }
}
