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
    public class TestServiceTests
    {
        private readonly Bootstrap.Mapping.AutoMapper mapper = new Bootstrap.Mapping.AutoMapper();

        [Fact]
        public void Should_Get_Item()
        {
            var singleTaskId = 6;
            var singleLevelTemplateId = 3;
            var testGet = new Entities.Test
            {
                Id = 1,
                Levels =
                {
                    new TestLevel
                    {
                        TestTemplateLevelId = singleLevelTemplateId,
                        Tasks =
                        {
                            new TestLevelTask
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

            var repositoryMock = new Mock<IRepository<Entities.Test>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(testGet);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => repositoryMock.Object);

            var service = new TestService(null, unitOfWorkMock.Object, mapper);

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
            var repositoryMock = new Mock<IRepository<Entities.Test>>();
            repositoryMock.Setup(u => u.Get(1)).Returns<Entities.Test>(null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => repositoryMock.Object);

            var service = new TestService(null, unitOfWorkMock.Object, mapper);

            Assert.Throws<EntityNotFoundException<Entities.Test>>(() => service.GetById(1));

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Delete()
        {
            var repositoryMock = new Mock<IRepository<Entities.Test>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => repositoryMock.Object);

            var service = new TestService(null, unitOfWorkMock.Object, mapper);

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
            var testUpdate = new TestDTO
            {
                Id = 1,
                Title = "Sample",
                Levels =
                {
                    new TestLevelDTO
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

            var repositoryMock = new Mock<IRepository<Entities.Test>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => repositoryMock.Object);

            var service = new TestService(null, unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(testUpdate);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Update(It.Is<Entities.Test>(t =>
                t.Id == testUpdate.Id
                && t.Title == testUpdate.Title
                && t.Levels.Single().TestTemplateLevelId == singleLevelTemplateId
                && t.Levels.Single().Description == testUpdate.Levels.Single().Description
                && t.Levels.Single().Tasks.Single().TaskId == singleTaskId)));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Create()
        {
            var singleTaskId = 6;
            var singleLevelTemplateId = 3;
            var testCreate = new TestDTO
            {
                Id = 1,
                Title = "Sample",
                Levels =
                {
                    new TestLevelDTO
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
            var repositoryMock = new Mock<IRepository<Entities.Test>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => repositoryMock.Object);

            var service = new TestService(null, unitOfWorkMock.Object, mapper);

            await service.CreateAsync(testCreate);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Create(It.Is<Entities.Test>(t =>
                t.Id == testCreate.Id
                && t.Title == testCreate.Title
                && t.Levels.Single().TestTemplateLevelId == singleLevelTemplateId
                && t.Levels.Single().Description == testCreate.Levels.Single().Description
                && t.Levels.Single().Tasks.Single().TaskId == singleTaskId)));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Generate_Title_And_TemplateId_On_Bind_To_Template()
        {
            var templateToBind = new TestTemplate { Id = 1 };
            var testToBeBinded = new TestDTO();

            var testsRepositoryMock = new Mock<IRepository<Entities.Test>>();
            testsRepositoryMock.Setup(m => m.Filter(It.IsAny<Expression<Func<Entities.Test, bool>>>()))
                .Returns(new Entities.Test[3]);

            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.Get(1)).Returns(templateToBind);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => testsRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var service = new TestService(null, unitOfWorkMock.Object, mapper);
            service.BindToTemplate(testToBeBinded, 1);

            Assert.Equal(templateToBind.Id, testToBeBinded.TestTemplateId);
            Assert.Equal("Test #4", testToBeBinded.Title);
        }

        [Fact]
        public void Should_Generate_Level_On_Bind_To_Template()
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
            var testToBeBinded = new TestDTO();

            var testsRepositoryMock = new Mock<IRepository<Entities.Test>>();
            testsRepositoryMock.Setup(m => m.Filter(It.IsAny<Expression<Func<Entities.Test, bool>>>()))
                .Returns(new Entities.Test[0]);

            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.Get(1)).Returns(templateToBind);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => testsRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var taskServiceMock = new Mock<ITaskSource>();
            taskServiceMock.Setup(m => m.Filter(It.IsAny<TaskFilterDTO>())).Returns(new TaskDTO[3]);

            var service = new TestService(taskServiceMock.Object, unitOfWorkMock.Object, mapper);
            service.BindToTemplate(testToBeBinded, 1);

            Assert.Single(testToBeBinded.Levels);
            Assert.False(testToBeBinded.Levels.Single().TemplateModified);
            Assert.False(testToBeBinded.Levels.Single().TemplateDeleted);
            Assert.Equal(templateToBind.Levels.Single().Description, testToBeBinded.Levels.Single().Description);
            Assert.Equal(templateToBind.Levels.Single().Id, testToBeBinded.Levels.Single().TestTemplateLevelId);
            Assert.Equal(testToBeBinded.Id, testToBeBinded.Levels.Single().TestId);
            Assert.Equal(3, testToBeBinded.Levels.Single().AvailableTasks.Count);
        }

        [Fact]
        public void Should_Update_Level_On_Bind_To_Template()
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
            var testToBeBinded = new TestDTO()
            {
                TestTemplateId = 1,
                Levels =
                {
                    new TestLevelDTO
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

            var testsRepositoryMock = new Mock<IRepository<Entities.Test>>();
            testsRepositoryMock.Setup(m => m.Filter(It.IsAny<Expression<Func<Entities.Test, bool>>>()))
                .Returns(new Entities.Test[0]);

            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.Get(1)).Returns(templateToBind);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => testsRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var taskServiceMock = new Mock<ITaskSource>();
            taskServiceMock.Setup(m => m.Filter(It.IsAny<TaskFilterDTO>()))
                .Returns(new[] { new TaskDTO { Id = 4 }, new TaskDTO { Id = 5 } });

            var service = new TestService(taskServiceMock.Object, unitOfWorkMock.Object, mapper);
            service.BindToTemplate(testToBeBinded, 1);

            Assert.Single(testToBeBinded.Levels);
            Assert.True(testToBeBinded.Levels.Single().TemplateModified);
            Assert.False(testToBeBinded.Levels.Single().TemplateDeleted);
            Assert.Equal(2, testToBeBinded.Levels.Single().AvailableTasks.Count);
        }

        [Fact]
        public void Should_Delete_Level_On_Bind_To_Template()
        {
            var templateToBind = new TestTemplate
            {
                Id = 1
            };
            var testToBeBinded = new TestDTO()
            {
                TestTemplateId = 1,
                Levels =
                {
                    new TestLevelDTO
                    {
                        TestTemplateLevelId = 3
                    }
                }
            };

            var testsRepositoryMock = new Mock<IRepository<Entities.Test>>();
            testsRepositoryMock.Setup(m => m.Filter(It.IsAny<Expression<Func<Entities.Test, bool>>>()))
                .Returns(new Entities.Test[0]);

            var templatesRepositoryMock = new Mock<IRepository<TestTemplate>>();
            templatesRepositoryMock.Setup(m => m.Get(1)).Returns(templateToBind);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => testsRepositoryMock.Object);
            unitOfWorkMock.Setup(u => u.TestTemplates).Returns(() => templatesRepositoryMock.Object);

            var service = new TestService(null, unitOfWorkMock.Object, mapper);
            service.BindToTemplate(testToBeBinded, 1);

            Assert.Single(testToBeBinded.Levels);
            Assert.True(testToBeBinded.Levels.Single().TemplateDeleted);
        }

        [Fact]
        public async Task Should_Delete_Level_With_Deleted_Template_On_Update()
        {
            var singleLevelTemplateId = 3;
            var testUpdate = new TestDTO
            {
                Levels =
                {
                    new TestLevelDTO
                    {
                        TestTemplateLevelId = null,
                        TemplateDeleted = true
                    },
                    new TestLevelDTO
                    {
                        TestTemplateLevelId = singleLevelTemplateId,
                        Tasks =
                        {
                            new TaskDTO()
                        }
                    }
                }
            };

            var repositoryMock = new Mock<IRepository<Entities.Test>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tests).Returns(() => repositoryMock.Object);

            var service = new TestService(null, unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(testUpdate);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Update(It.Is<Entities.Test>(t =>
                t.Levels.Single().TestTemplateLevelId == singleLevelTemplateId)));
            repositoryMock.VerifyNoOtherCalls();
        }
    }
}
