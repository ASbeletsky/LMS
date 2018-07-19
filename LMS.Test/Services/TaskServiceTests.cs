using System;
using System.Linq;
using System.Threading.Tasks;
using LMS.Dto;
using LMS.Interfaces;
using LMS.Business.Services;
using Moq;
using Xunit;

namespace LMS.Test.Services
{
    public class TaskServiceTests
    {
        private readonly Bootstrap.Mapping.AutoMapper mapper = new Bootstrap.Mapping.AutoMapper();

        [Fact]
        public void Should_Get_Item()
        {
            var taskGet = new Entities.Task
            {
                Id = 1,
                IsActive = true,
                CategoryId = 1,
                TypeId = 1,
                Complexity = 1,
                Content = "Sample"
            };

            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(taskGet);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);

            var actualGet = service.GetById(1);
            Assert.NotNull(actualGet);
            Assert.Equal(taskGet.Content, actualGet.Content);
            Assert.True(actualGet.IsActive);
            Assert.Equal(taskGet.IsActive, actualGet.IsActive);
            Assert.Equal(taskGet.CategoryId, actualGet.CategoryId);
            Assert.Equal(taskGet.TypeId, actualGet.TypeId);
            Assert.Equal(taskGet.Complexity, actualGet.Complexity);
            Assert.Equal(taskGet.Content, actualGet.Content);

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void Should_Throw_NotFound_On_Get()
        {
            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(u => u.Get(1)).Throws<EntityNotFoundException<Entities.Task>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);

            Assert.Throws<EntityNotFoundException<Entities.Task>>(() => service.GetById(1));

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Mark_As_Not_Active_On_Delete()
        {
            var taskForDelete = new Entities.Task
            {
                Id = 1,
                IsActive = true
            };

            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(taskForDelete);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, null);

            await service.MarkAsDeletedByIdAsync(1);

            unitOfWorkMock.Verify(m => m.SaveAsync());
            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
            Assert.False(taskForDelete.IsActive);
        }

        [Fact]
        public async Task Should_Create_If_New_On_Update()
        {
            var newTask = new TaskDTO
            {
                Id = 1,
                IsActive = true,
                CategoryId = 1,
                TypeId = 1,
                Complexity = 1, 
                Content = "Sample"
            };

            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(u => u.Get(1)).Returns((Entities.Task)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(newTask);

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.Verify(m => m.Create(It.Is<Entities.Task>(dto => 
                dto.IsActive
                && dto.Content == newTask.Content
                && dto.Complexity == newTask.Complexity
                && dto.CategoryId == newTask.CategoryId
                && dto.TypeId == newTask.TypeId
                && dto.PreviousVersion == null)));
            repositoryMock.VerifyNoOtherCalls();
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }

        [Fact]
        public async Task Should_Not_Make_Any_Change_If_Not_Updated()
        {
            var oldTask = new Entities.Task
            {
                Id = 1,
                IsActive = true,
                CategoryId = 1,
                TypeId = 1,
                Complexity = 1,
                Content = "Sample"
            };
            var notUpdatedDtoItem = mapper.Map<Entities.Task, TaskDTO>(oldTask);

            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(oldTask);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(notUpdatedDtoItem);

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.VerifyNoOtherCalls();
            unitOfWorkMock.VerifyGet(m => m.Tasks);
            unitOfWorkMock.VerifyNoOtherCalls();
            Assert.True(oldTask.IsActive);
        }

        [Fact]
        public async Task Should_Create_New_And_Mark_Old_On_Updated()
        {
            var oldTask = new Entities.Task
            {
                Id = 1,
                IsActive = true,
                CategoryId = 1,
                TypeId = 1,
                Complexity = 1,
                Content = "Sample"
            };
            var updatedDtoItem = mapper.Map<Entities.Task, TaskDTO>(oldTask);
            updatedDtoItem.Content = "Sample 2";

            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(oldTask);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(updatedDtoItem);

            repositoryMock.Verify(m => m.Get(1));
            repositoryMock.Verify(m => m.Update(It.Is<Entities.Task>(t => 
                !t.IsActive && t.Content == oldTask.Content)));
            repositoryMock.Verify(m => m.Create(It.Is<Entities.Task>(t => 
                t.IsActive 
                && t.Content == updatedDtoItem.Content
                && t.Complexity == updatedDtoItem.Complexity
                && t.TypeId == updatedDtoItem.TypeId
                && t.CategoryId == updatedDtoItem.CategoryId
                && t.PreviousVersion.Id == oldTask.Id)));
            repositoryMock.VerifyNoOtherCalls();
            unitOfWorkMock.Verify(m => m.SaveAsync());
            Assert.False(oldTask.IsActive);
        }

        [Fact]
        public async Task Should_Create_New_Item()
        {
            var newTask = new TaskDTO
            {
                Id = 1,
                IsActive = true,
                CategoryId = 1,
                TypeId = 1,
                Complexity = 1,
                Content = "Sample"
            };
            var repositoryMock = new Mock<IRepository<Entities.Task>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);

            await service.CreateAsync(newTask);

            repositoryMock.Verify(m => m.Create(It.Is<Entities.Task>(t =>
                t.IsActive
                && t.Content == newTask.Content
                && t.Complexity == newTask.Complexity
                && t.TypeId == newTask.TypeId
                && t.CategoryId == newTask.CategoryId)));
            repositoryMock.VerifyNoOtherCalls();
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }

        [Fact]
        public async Task Should_Create_New_And_Get_Item()
        {
            var newTask = new TaskDTO
            {
                Id = 1,
                IsActive = true,
                CategoryId = 1,
                TypeId = 1,
                Complexity = 1,
                Content = "Sample"
            };

            Entities.Task tempTask = null;
            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(r => r.Create(It.IsAny<Entities.Task>()))
                .Callback(new Action<Entities.Task>(task => tempTask = task));
            repositoryMock.Setup(r => r.Get(It.IsAny<int>()))
                .Returns(() => tempTask);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);

            await service.CreateAsync(newTask);

            var createdTask =  service.GetById(1);
            Assert.True(createdTask.IsActive);
            Assert.Equal(newTask.IsActive, createdTask.IsActive);
            Assert.Equal(newTask.CategoryId, createdTask.CategoryId);
            Assert.Equal(newTask.TypeId, createdTask.TypeId);
            Assert.Equal(newTask.Complexity, createdTask.Complexity);
            Assert.Equal(newTask.Content, createdTask.Content);
        }

        [Fact]
        public void Should_Get_All_Active()
        {
            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(m => m.GetAll()).Returns(() => new[]
            {
                new Entities.Task { IsActive = true },
                new Entities.Task { IsActive = false },
                new Entities.Task { IsActive = true }
            });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);
            var tasks = service.GetAll().ToArray();
            Assert.Equal(2, tasks.Length);
            Assert.True(tasks.All(t => t.IsActive));
        }

        [Fact]
        public void Should_Get_All()
        {
            var repositoryMock = new Mock<IRepository<Entities.Task>>();
            repositoryMock.Setup(m => m.GetAll()).Returns(() => new[]
            {
                new Entities.Task { IsActive = true },
                new Entities.Task { IsActive = false },
                new Entities.Task { IsActive = true }
            });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMock.Object);

            var service = new TaskService(unitOfWorkMock.Object, mapper);
            var tasks = service.GetAll(includeInactive: true).ToArray();
            Assert.Equal(3, tasks.Length);
        }
    }
}
