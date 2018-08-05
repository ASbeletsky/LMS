using System;
using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TestSessionUserService : BaseService
    {
        public TestSessionUserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public TestSessionUser GetById(int sessionId, string userId)
        {
            var template = unitOfWork.TestSessionUser.Find(t =>
            t.SessionId == sessionId
            && t.UserId == userId);
            if (template == null)
            {
                throw new EntityNotFoundException<TestSessionUser>();
            }
            return template;
        }

        //public TestSessionUser GetByCode(string Code)
        //{
        //    var template = unitOfWork.TestSessionUser.Filter(t =>
        //(t.Code == Code && t.Session.StartTime.Add(new TimeSpan(0, 15, 0)) >= DateTimeOffset.Now)
        //    || (t.Code == Code && t.TestId != null && t.Ended == false && t.EndTime > DateTimeOffset.Now));
        //    if (template.Count() == 0)
        //    {
        //        return null;
        //    }
        //    return ChoseTest(template.First());
        //}

        public TestSessionUser GetByUserId(string user)
        {
            var template = unitOfWork.TestSessionUser.Filter(t =>
            (t.UserId == user && t.Session.StartTime.Subtract(new TimeSpan(0, 15, 0)) <= DateTimeOffset.Now
            && t.Session.StartTime.Add(t.Session.Duration) > DateTimeOffset.Now)
            || (t.UserId == user && t.TestId != null));
            //&& t.Ended == false && t.EndTime > DateTimeOffset.Now
            if (template.Count() == 0)
            {
                return null;
            }
            return ChoseTest(template.First());
        }

        public TestSessionUser ChoseTest(TestSessionUser userSession)
        {
            if (userSession.TestId == null)
            {
                var session = unitOfWork.TestSessions.Get(userSession.SessionId);
                var tests = session.Tests;
                var members = session.Members;
                var UsedTests = new Dictionary<int, int>();
                int recomend = 0;
                foreach (var test in tests)
                {
                    var used = members.Where(t => t.TestId == test.TestId);
                    if (used == null || used.Count() == 0)
                    {
                        recomend = test.TestId;
                        break;
                    }
                    UsedTests.Add(test.TestId, used.Count());
                }
                if (recomend == 0)
                {
                    recomend = UsedTests.Min().Key;
                }
                userSession.TestId = recomend;
                UpdateAsync(userSession);
            }
            return userSession;
        }

        public Task DeleteByIdAsync(int id)
        {
            unitOfWork.TestSessionUser.Delete(id);
            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TestSessionUser testSession)
        {
            if (testSession == null)
            {
                throw new ArgumentNullException(nameof(testSession));
            }
            unitOfWork.TestSessionUser.Update(testSession);
            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(TestSessionUser testSession)
        {
            if (testSession == null)
            {
                throw new ArgumentNullException(nameof(testSession));
            }
            unitOfWork.TestSessionUser.Create(testSession);
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TestSessionUser> GetAll()
        {
            return unitOfWork.TestSessionUser.GetAll();
        }

    }
}
