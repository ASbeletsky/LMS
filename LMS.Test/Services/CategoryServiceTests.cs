using System;
using System.Linq;
using System.Threading.Tasks;
using LMS.Dto;
using LMS.Interfaces;
using LMS.Business.Services;
using Moq;
using Xunit;
using System.Linq.Expressions;

namespace LMS.Test.Services
{
    public class CategoryServiceTests
    {
        private readonly Bootstrap.Mapping.AutoMapper mapper = new Bootstrap.Mapping.AutoMapper();

        [Fact]
        public void Should_Get_All()
        {

            var repositoryMock = new Mock<IRepository<Entities.Category>>(MockBehavior.Default);
            repositoryMock.Setup(m => m.GetAll()).Returns(() => new[]
            {
                new Entities.Category { Id=0, Title="MyCategory"  },
            });

            var repositoryMockForTasks = new Mock<IRepository<Entities.Task>>(MockBehavior.Default);
            repositoryMockForTasks.Setup(m => m.Filter(It.Is<Expression<Func<Entities.Task, bool>>>((n => true)))).Returns(() => new[]
            {
                    new Entities.Task { Id=0, CategoryId=0  },
                    new Entities.Task { Id=1, CategoryId=0  },
                    new Entities.Task { Id=2, CategoryId=0  },
            });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Categories).Returns(() => repositoryMock.Object);
            unitOfWorkMock.Setup(u => u.Tasks).Returns(() => repositoryMockForTasks.Object);

            var service = new CategoryService(unitOfWorkMock.Object, mapper);

            var categories = service.GetAll().ToArray();

            Assert.NotEqual(0, categories.Length);
            Assert.Equal(3, categories[0].TasksCount);
            repositoryMock.Verify(m => m.GetAll());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Delete()
        {
            var categoryForDelete = new Entities.Category
            {
                Id = 1,
                Title = "MyCategory"
            };

            var repositoryMock = new Mock<IRepository<Entities.Category>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(categoryForDelete);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Categories).Returns(() => repositoryMock.Object);

            var service = new CategoryService(unitOfWorkMock.Object, null);

            await service.DeleteAsync(1);

            repositoryMock.Verify(m => m.Delete(1));
            unitOfWorkMock.Verify(m => m.SaveAsync());

        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Update()
        {
            var oldCategory = new Entities.Category
            {
                Id = 1,
                Title = "Category 1"
            };
            var newCategoryDto = mapper.Map<Entities.Category, CategoryDTO>(oldCategory);
            newCategoryDto.Title = "Category 2";

            var repositoryMock = new Mock<IRepository<Entities.Category>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(oldCategory);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Categories).Returns(() => repositoryMock.Object);

            var service = new CategoryService(unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(newCategoryDto);

            repositoryMock.Verify(m => m.Update(It.Is<Entities.Category>(t => t.Title == newCategoryDto.Title)));
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Create_If_New_On_Update()
        {

            var newCategory = new Entities.Category
            {
                Id = 1,
                Title = "MyCategory"
            };
            var newCategoryDto = mapper.Map<Entities.Category, CategoryDTO>(newCategory);

            var repositoryMock = new Mock<IRepository<Entities.Category>>();
            repositoryMock.Setup(u => u.Get(1)).Returns((Entities.Category)null);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Categories).Returns(() => repositoryMock.Object);

            var service = new CategoryService(unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(newCategoryDto);

            repositoryMock.Verify(m => m.Create(It.Is<Entities.Category>(dto => dto.Title == newCategory.Title)));
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Create_New_Item()
        {
            var newCategory = new CategoryDTO
            {
                Id = 1,
                Title = "MyCategory"
            };

            var repositoryMock = new Mock<IRepository<Entities.Category>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Categories).Returns(() => repositoryMock.Object);

            var service = new CategoryService(unitOfWorkMock.Object, mapper);

            await service.CreateAsync(newCategory);

            repositoryMock.Verify(m => m.Create(It.Is<Entities.Category>(t => t.Title == newCategory.Title)));
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }

        [Fact]
        public void Should_Get_Item()
        {
            var categoryGet = new Entities.Category
            {
                Id = 1,
                Title = "MyCategory"
            };

            var repositoryMock = new Mock<IRepository<Entities.Category>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(categoryGet);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Categories).Returns(() => repositoryMock.Object);

            var service = new CategoryService(unitOfWorkMock.Object, mapper);

            var actualGet = service.GetById(1);

            Assert.NotNull(actualGet);
            Assert.Equal(categoryGet.Title, actualGet.Title);

            repositoryMock.Verify(m => m.Get(1));
        }
    }
}
