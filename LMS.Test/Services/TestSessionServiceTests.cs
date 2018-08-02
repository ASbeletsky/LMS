using System.Linq;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;
using LMS.Business.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace LMS.Test.Services
{
    public class TestSessionServiceTests
    {
        private readonly Bootstrap.Mapping.AutoMapper mapper = new Bootstrap.Mapping.AutoMapper();

        [Fact]
        public void Should_Get_All()
        {
            var repositoryMock = new Mock<IRepository<TestSession>>(MockBehavior.Default);
            repositoryMock.Setup(m => m.GetAll()).Returns(() => new[]
            {
                new TestSession { Id=0, Title="My session" },
                new TestSession { Id=2, Title="My another session" }
            });

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestSessions).Returns(() => repositoryMock.Object);

            var service = new TestSessionService(unitOfWorkMock.Object, mapper);

            var categories = service.GetAll().ToArray();

            Assert.Equal(2, categories.Length);
            Assert.Equal("My another session", categories[1].Title);
            repositoryMock.Verify(m => m.GetAll());
            unitOfWorkMock.VerifyGet(m => m.TestSessions);
            unitOfWorkMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Should_Delete()
        {
            var sessionForDelete = new TestSession
            {
                Id = 1,
                Title = "My session"
            };

            var repositoryMock = new Mock<IRepository<TestSession>>(MockBehavior.Default);
            repositoryMock.Setup(u => u.Get(1)).Returns(sessionForDelete);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestSessions).Returns(() => repositoryMock.Object);

            var service = new TestSessionService(unitOfWorkMock.Object, null);

            await service.DeleteByIdAsync(1);

            repositoryMock.Verify(m => m.Delete(1));
            repositoryMock.VerifyNoOtherCalls();
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }

        [Fact]
        public async Task Should_Update()
        {
            var sessionToUpdate = new TestSession
            {
                Id = 1,
                Title = "Session 1",
                Tests =
                {
                    new TestSessionTest
                    {
                        TestId = 2
                    }
                },
                Members =
                {
                    new TestSessionUser
                    {
                        UserId = "3"
                    }
                }
            };
            var updatedSessionDTO = mapper.Map<TestSession, TestSessionDTO>(sessionToUpdate);
            updatedSessionDTO.Title = "Session 2";

            var repositoryMock = new Mock<IRepository<TestSession>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(sessionToUpdate);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestSessions).Returns(() => repositoryMock.Object);

            var service = new TestSessionService(unitOfWorkMock.Object, mapper);

            await service.UpdateAsync(updatedSessionDTO);

            repositoryMock.Verify(m => m.Update(It.Is<TestSession>(t =>
                t.Id == sessionToUpdate.Id
                && t.Title == updatedSessionDTO.Title
                && t.Tests.Single().TestId == sessionToUpdate.Tests.Single().TestId
                && t.Members.Single().UserId == sessionToUpdate.Members.Single().UserId)));
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }

        [Fact]
        public async Task Should_Create_New_Item()
        {
            var sessionToCreate = new TestSessionDTO
            {
                Id = 1,
                Title = "My session",
                TestIds =
                {
                    2
                },
                MemberIds =
                {
                    "3"
                }
            };

            var repositoryMock = new Mock<IRepository<TestSession>>();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestSessions).Returns(() => repositoryMock.Object);

            var service = new TestSessionService(unitOfWorkMock.Object, mapper);

            await service.CreateAsync(sessionToCreate);

            repositoryMock.Verify(m => m.Create(It.Is<TestSession>(t =>
                t.Title == sessionToCreate.Title
                && t.Tests.Single().TestId == sessionToCreate.TestIds.Single()
                && t.Members.Single().UserId == sessionToCreate.MemberIds.Single())));
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }

        [Fact]
        public void Should_Get_Item()
        {
            var sessionToGet = new TestSession
            {
                Id = 1,
                Title = "My session"
            };

            var repositoryMock = new Mock<IRepository<TestSession>>();
            repositoryMock.Setup(u => u.Get(1)).Returns(sessionToGet);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.TestSessions).Returns(() => repositoryMock.Object);

            var service = new TestSessionService(unitOfWorkMock.Object, mapper);

            var actualGet = service.GetById(1);

            Assert.NotNull(actualGet);
            Assert.Equal(sessionToGet.Title, actualGet.Title);

            repositoryMock.Verify(m => m.Get(1));
        }

        [Fact]
        public async Task Should_Save_Answers_Scores()
        {
            var testList = new List<TaskAnswer>
            {
                new TaskAnswer
                {
                    Id = 1,
                    Score = 5,
                    Content = "#5"
                },
                new TaskAnswer
                {
                    Id = 3,
                    Score = 9,
                    Content = "#7"
                }
            };

            var updatedScores = new List<TaskAnswerScoreDTO>
            {
                new TaskAnswerScoreDTO { Id = 1, Score = 3 },
                new TaskAnswerScoreDTO { Id = 3, Score = 8 }
            };

            var repositoryMock = new Mock<IRepository<TaskAnswer>>();
            repositoryMock.Setup(u => u
                .Filter(It.IsAny<Expression<Func<TaskAnswer, bool>>>()))
                .Returns(testList);

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.Answers).Returns(() => repositoryMock.Object);

            var service = new TestSessionService(unitOfWorkMock.Object, mapper);

            await service.SaveAnswerScoresAsync(updatedScores);

            repositoryMock.Verify(m => m.Update(It.Is<TaskAnswer>(a =>
                (a.Id == 1 && a.Score == updatedScores[0].Score && a.Content == testList[0].Content)
                || (a.Id == 3 && a.Score == updatedScores[1].Score && a.Content == testList[1].Content))));
            unitOfWorkMock.Verify(m => m.SaveAsync());
        }
    }
}
