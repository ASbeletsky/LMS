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
    public class TestVariantServiceTests
    {
        private readonly Bootstrap.Mapping.AutoMapper mapper = new Bootstrap.Mapping.AutoMapper();

        [Fact]
        public void Should_Get_Item()
        {
            var templateGet = new TestVariant
            {
                Id = 1,
                Levels =
                {
                    new TestVariantLevel
                    {
                        TestTemplateLevelId = 3,
                        Tasks =
                        {
                            new TestVariantLevelTask
                            {
                                TaskId = 6
                            }
                        }
                    }
                }
            };

            var repositoryMock = new Mock<IRepository<TestVariant>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(templateGet);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => repositoryMock.Object);

            var taskServiceMock = new Mock<ITaskSource>();
            taskServiceMock.Setup(m => m.Filter(It.IsAny<TaskFilterDTO>())).Returns(new TaskDTO[3]);

            var service = new TestVariantService(taskServiceMock.Object, unitOfWorkMock.Object, mapper);

            var actualGet = service.GetById(1);
            Assert.NotNull(actualGet);
            Assert.Equal(3, actualGet.Levels.Single().TestTemplateLevelId);
            Assert.Single(actualGet.Levels.Single().Tasks);
            Assert.Equal(6, actualGet.Levels.Single().Tasks.Single().Id);
            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Throw_NotFound_On_Get()
        {
            var repositoryMock = new Mock<IRepository<TestVariant>>();
            repositoryMock.Setup(u => u.Get(1)).Returns<TestVariant>(null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => repositoryMock.Object);

            var service = new TestVariantService(null, unitOfWorkMock.Object, mapper);

            Assert.Throws<EntityNotFoundException<TestTemplate>>(() => service.GetById(1));

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Delete()
        {
            var templateDelete = new TestVariant
            {
                Id = 1
            };

            var repositoryMock = new Mock<IRepository<TestVariant>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => repositoryMock.Object);

            var service = new TestVariantService(null, unitOfWorkMock.Object, mapper);

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
            var templateUpdate = new TestVariantDTO
            {
                Id = 1,
                Title = "Sample",
                Levels =
                {
                    new TestVariantLevelDTO
                    {
                        Description = "Level desc",
                        Tasks =
                        {

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
    }
}
