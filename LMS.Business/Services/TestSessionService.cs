using System;
using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TestSessionService : BaseService
    {
        public TestSessionService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public TestSessionDTO GetById(int id)
        {
            var template = unitOfWork.TestSessions.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TestSession>(id);
            }

            return mapper.Map<TestSession, TestSessionDTO>(template);
        }

        public TestSessionResultsDTO GetResults(int id)
        {
            var template = unitOfWork.TestSessions.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TestSession>(id);
            }

            return mapper.Map<TestSession, TestSessionResultsDTO>(template);
        }

        public ExameneeResultDTO GetExameneeResult(int sessionId, string exameneeId)
        {
            var template = unitOfWork.TestSessions.Get(sessionId);
            if (template == null)
            {
                throw new EntityNotFoundException<TestSession>(sessionId);
            }

            var user = template.Members.FirstOrDefault(m => m.UserId == exameneeId);
            if (user == null)
            {
                throw new EntityNotFoundException<TestSessionUser>();
            }

            var exameneeResult = mapper.Map<TestSessionUser, ExameneeResultDTO>(user);
            if (!exameneeResult.TestId.HasValue)
            {
                return exameneeResult;
            }

            var test = unitOfWork.Tests.Get(exameneeResult.TestId.Value);
            var levels = test.Levels;
            var templateLevels = unitOfWork.TestTemplates.Get(test.Id).Levels;

            foreach (var task in exameneeResult.Answers.Select(a => a.Task))
            {
                var templateLevelId = levels
                    .FirstOrDefault(l => l.Tasks.Select(t => t.TaskId).Contains(task.Id))?
                    .TestTemplateLevelId;
                var templateLevel = templateLevels.FirstOrDefault(l => l.Id == templateLevelId);
                if (templateLevel != null)
                {
                    task.MaxScore = templateLevel.MaxScore / templateLevel.TasksCount;
                }
            }

            return exameneeResult;
        }

        public Task DeleteByIdAsync(int id)
        {
            unitOfWork.TestSessions.Delete(id);
            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TestSessionDTO testSessionDTO)
        {
            if (testSessionDTO == null)
            {
                throw new ArgumentNullException(nameof(testSessionDTO));
            }
            if (!testSessionDTO.TestIds.Any())
            {
                throw new ArgumentException("Test session should contains at least one test");
            }
            if (!testSessionDTO.MemberIds.Any())
            {
                throw new ArgumentException("Test session shoul contains at least one examenee");
            }

            var testSession = mapper.Map<TestSessionDTO, TestSession>(testSessionDTO);

            unitOfWork.TestSessions.Update(testSession);

            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(TestSessionDTO testSessionDTO)
        {
            if (testSessionDTO == null)
            {
                throw new ArgumentNullException(nameof(testSessionDTO));
            }
            if (!testSessionDTO.TestIds.Any())
            {
                throw new ArgumentException("Test session should contains at least one test");
            }
            if (!testSessionDTO.MemberIds.Any())
            {
                throw new ArgumentException("Test session shoul contains at least one examenee");
            }

            var testSession = mapper.Map<TestSessionDTO, TestSession>(testSessionDTO);

            unitOfWork.TestSessions.Create(testSession);

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TestSessionDTO> GetAll()
        {
            return mapper
                .Map<IEnumerable<TestSession>, IEnumerable<TestSessionDTO>>(
                    unitOfWork.TestSessions.GetAll());
        }

        public Task SaveAnswerScoresAsync(ICollection<TaskAnswerScoreDTO> taskAnswerScores)
        {
            var answers = unitOfWork.Answers.Filter(a => taskAnswerScores.Any(s => s.Id == a.Id));
            foreach (var answer in answers)
            {
                answer.Score = taskAnswerScores.First(a => a.Id == answer.Id).Score;
                unitOfWork.Answers.Update(answer);
            }
            return unitOfWork.SaveAsync();
        }

        public TestSessionDTO FindByUserId(string userId)
        {
            return mapper.Map<TestSession, TestSessionDTO>(unitOfWork
                .TestSessions.Find(s => s.Members.Any(m => m.UserId == userId)));
        }
    }
}
