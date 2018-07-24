using System;
using System.Linq;
using System.Linq.Expressions;
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
            var singleTaskId = 6;
            var singleLevelTemplateId = 3;
            var variantGet = new TestVariant
            {
                Id = 1,
                Levels =
                {
                    new TestVariantLevel
                    {
                        TestTemplateLevelId = singleLevelTemplateId,
                        Tasks =
                        {
                            new TestVariantLevelTask
                            {
                                Task = new Entities.Task
                                {
                                    Id = singleTaskId
                                }
                            }
                        }
                    }
                }
            };

            var repositoryMock = new Mock<IRepository<TestVariant>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(variantGet);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => repositoryMock.Object);

            var service = new TestVariantService(null, unitOfWorkMock.Object, mapper);

            var actualGet = service.GetById(1);
            Assert.NotNull(actualGet);
            Assert.Equal(singleLevelTemplateId, actualGet.Levels.Single().TestTemplateLevelId);
            Assert.Single(actualGet.Levels.Single().Tasks);
            Assert.Equal(singleTaskId, actualGet.Levels.Single().Tasks.Single().Id);
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

            Assert.Throws<EntityNotFoundException<TestVariant>>(() => service.GetById(1));

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Delete()
        {
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
            var singleTaskId = 6;
            var singleLevelTemplateId = 3;
            var variantUpdate = new TestVariantDTO
            {
                Id = 1,
                Title = "Sample",
                Levels =
                {
                    new TestVariantLevelDTO
                    {
                        TestTemplateLevelId = singleLevelTemplateId,
                        Description = "Level desc",
                        Tasks =
                        {
                            new TaskDTO
                            {
                                Id = singleTaskId
                            }
                        }
                    }
                }
            };

            var repositoryMock = new Mock<IRepository<TestVariant>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => repositoryMock.Object);

            var service = new TestVariantService(null, unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(variantUpdate);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Update(It.Is<TestVariant>(t =>
                t.Id == variantUpdate.Id
                && t.Title == variantUpdate.Title
                && t.Levels.Single().TestTemplateLevelId == singleLevelTemplateId
                && t.Levels.Single().Description == variantUpdate.Levels.Single().Description
                && t.Levels.Single().Tasks.Single().TaskId == singleTaskId)));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Create()
        {
            var singleTaskId = 6;
            var singleLevelTemplateId = 3;
            var variantCreate = new TestVariantDTO
            {
                Id = 1,
                Title = "Sample",
                Levels =
                {
                    new TestVariantLevelDTO
                    {
                        TestTemplateLevelId = singleLevelTemplateId,
                        Description = "Level desc",
                        Tasks =
                        {
                            new TaskDTO
                            {
                                Id = singleTaskId
                            }
                        }
                    }
                }
            };
            var repositoryMock = new Mock<IRepository<TestVariant>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => repositoryMock.Object);

            var service = new TestVariantService(null, unitOfWorkMock.Object, mapper);

            await service.CreateAsync(variantCreate);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Create(It.Is<TestVariant>(t =>
                t.Id == variantCreate.Id
                && t.Title == variantCreate.Title
                && t.Levels.Single().TestTemplateLevelId == singleLevelTemplateId
                && t.Levels.Single().Description == variantCreate.Levels.Single().Description
                && t.Levels.Single().Tasks.Single().TaskId == singleTaskId)));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Shoul_Generate_Title_And_TemplateId_On_Bind_To_Template()
        {
            var templateToBind = new TestTemplate { Id = 1 };
            var variantToBeBinded = new TestVariantDTO();

            var variantesRepositoryMock = new Mock<IRepository<TestVariant>>();
            variantesRepositoryMock.Setup(m => m.Filter(It.IsAny<Expression<Func<TestVariant, bool>>>()))
                .Returns(new TestVariant[3]);

            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.Get(1)).Returns(templateToBind);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => variantesRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var service = new TestVariantService(null, unitOfWorkMock.Object, mapper);
            service.BindToTemplate(variantToBeBinded, 1);

            Assert.Equal(templateToBind.Id, variantToBeBinded.TestTemplateId);
            Assert.Equal("Variant #4", variantToBeBinded.Title);
        }

        [Fact]
        public void Shoul_Generate_Level_On_Bind_To_Template()
        {
            var templateToBind = new TestTemplate
            {
                Id = 1,
                Levels =
                {
                    new TestTemplateLevel
                    {
                        Id = 1,
                        Description = "Desc 1"
                    }
                }
            };
            var variantToBeBinded = new TestVariantDTO();

            var variantesRepositoryMock = new Mock<IRepository<TestVariant>>();
            variantesRepositoryMock.Setup(m => m.Filter(It.IsAny<Expression<Func<TestVariant, bool>>>()))
                .Returns(new TestVariant[0]);

            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.Get(1)).Returns(templateToBind);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => variantesRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var taskServiceMock = new Mock<ITaskSource>();
            taskServiceMock.Setup(m => m.Filter(It.IsAny<TaskFilterDTO>())).Returns(new TaskDTO[3]);

            var service = new TestVariantService(taskServiceMock.Object, unitOfWorkMock.Object, mapper);
            service.BindToTemplate(variantToBeBinded, 1);

            Assert.Single(variantToBeBinded.Levels);
            Assert.False(variantToBeBinded.Levels.Single().TemplateModified);
            Assert.False(variantToBeBinded.Levels.Single().TemplateDeleted);
            Assert.Equal(templateToBind.Levels.Single().Description, variantToBeBinded.Levels.Single().Description);
            Assert.Equal(templateToBind.Levels.Single().Id, variantToBeBinded.Levels.Single().TestTemplateLevelId);
            Assert.Equal(variantToBeBinded.Id, variantToBeBinded.Levels.Single().TestVariantId);
            Assert.Equal(3, variantToBeBinded.Levels.Single().AvailableTasks.Count);
        }

        [Fact]
        public void Shoul_Update_Level_On_Bind_To_Template()
        {
            var templateToBind = new TestTemplate
            {
                Id = 1,
                Levels =
                {
                    new TestTemplateLevel
                    {
                        Id = 2
                    }
                }
            };
            var variantToBeBinded = new TestVariantDTO()
            {
                TestTemplateId = 1,
                Levels =
                {
                    new TestVariantLevelDTO
                    {
                        TestTemplateLevelId = 2,
                        Tasks =
                        {
                            new TaskDTO
                            {
                                Id = 3
                            }
                        }
                    }
                }
            };

            var variantesRepositoryMock = new Mock<IRepository<TestVariant>>();
            variantesRepositoryMock.Setup(m => m.Filter(It.IsAny<Expression<Func<TestVariant, bool>>>()))
                .Returns(new TestVariant[0]);

            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.Get(1)).Returns(templateToBind);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => variantesRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var taskServiceMock = new Mock<ITaskSource>();
            taskServiceMock.Setup(m => m.Filter(It.IsAny<TaskFilterDTO>()))
                .Returns(new[] { new TaskDTO { Id = 4 }, new TaskDTO { Id = 5 } });

            var service = new TestVariantService(taskServiceMock.Object, unitOfWorkMock.Object, mapper);
            service.BindToTemplate(variantToBeBinded, 1);

            Assert.Single(variantToBeBinded.Levels);
            Assert.True(variantToBeBinded.Levels.Single().TemplateModified);
            Assert.False(variantToBeBinded.Levels.Single().TemplateDeleted);
            Assert.Equal(2, variantToBeBinded.Levels.Single().AvailableTasks.Count);
        }

        [Fact]
        public void Shoul_Delete_Level_On_Bind_To_Template()
        {
            var templateToBind = new TestTemplate
            {
                Id = 1
            };
            var variantToBeBinded = new TestVariantDTO()
            {
                TestTemplateId = 1,
                Levels =
                {
                    new TestVariantLevelDTO
                    {
                        TestTemplateLevelId = 3
                    }
                }
            };

            var variantesRepositoryMock = new Mock<IRepository<TestVariant>>();
            variantesRepositoryMock.Setup(m => m.Filter(It.IsAny<Expression<Func<TestVariant, bool>>>()))
                .Returns(new TestVariant[0]);

            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.Get(1)).Returns(templateToBind);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestVariants).Returns(() => variantesRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var service = new TestVariantService(null, unitOfWorkMock.Object, mapper);
            service.BindToTemplate(variantToBeBinded, 1);

            Assert.Single(variantToBeBinded.Levels);
            Assert.True(variantToBeBinded.Levels.Single().TemplateDeleted);
        }
    }
}
