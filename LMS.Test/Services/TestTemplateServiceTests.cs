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
                Id = 1
                /* More data */
            };

            var repositoryMock = new Mock<IRepository<TestTemplate>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(templateGet);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => repositoryMock.Object);

            var service = new TestTemplateService(null, unitOfWorkMock.Object, mapper);

            var actualGet = service.GetById(1);
            Assert.NotNull(actualGet);
            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Throw_NotFound_On_Get()
        {
            var repositoryMock = new Mock<IRepository<TestTemplate>>();
            repositoryMock.Setup(u => u.Get(1)).Throws<EntityNotFoundException<TestTemplate>>();

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
            var templateDelete = new TestTemplate
            {
                Id = 1
            };

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
            var templateUpdate = new TestTemplate
            {
                Id = 1
            };
            var mapped = mapper.Map<TestTemplate, TestTemplateDTO>(templateUpdate);

            var repositoryMock = new Mock<IRepository<TestTemplate>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => repositoryMock.Object);

            var service = new TestTemplateService(null, unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(mapped);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Update(It.Is<TestTemplate>(t =>
                t.Id == templateUpdate.Id
                && t.Title == templateUpdate.Title)));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Create()
        {
            var templateCreate = new TestTemplate
            {
                Id = 1
            };
            var mapped = mapper.Map<TestTemplate, TestTemplateDTO>(templateCreate);

            var repositoryMock = new Mock<IRepository<TestTemplate>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => repositoryMock.Object);

            var service = new TestTemplateService(null, unitOfWorkMock.Object, mapper);

            await service.CreateAsync(mapped);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Create(It.Is<TestTemplate>(t =>
                t.Id == templateCreate.Id
                && t.Title == templateCreate.Title)));
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
                            MaxComplexity = 3
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
                            MinComplexity = 5
                        }
                    }
                }
            });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var taskServiceMock = new Mock<TaskService>();
            taskServiceMock.Setup(m => m.GetByFilter(It.Is<TaskFilterDTO>(l => l.MinComplexity == 5)))
                .Returns(new TaskDTO[3]);
            taskServiceMock.Setup(m => m.GetByFilter(It.Is<TaskFilterDTO>(l => l.MaxComplexity == 3)))
                .Returns(new TaskDTO[2]);

            var service = new TestTemplateService(taskServiceMock.Object, unitOfWorkMock.Object, mapper);
            var testTemplateListItems = service.GetListItems().ToArray();
            Assert.Equal(3, testTemplateListItems.GroupBy(t => t.Id).Count());
            Assert.True(testTemplateListItems.All(t => t.Id.ToString() == t.Title));
            Assert.Equal(0, testTemplateListItems[0].Tasks.Count);
            Assert.Equal(2, testTemplateListItems[1].Tasks.Count);
            Assert.Equal(3, testTemplateListItems[2].Tasks.Count);
        }
    }
}
