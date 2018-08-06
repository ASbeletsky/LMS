using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LMS.Interfaces;
using LMS.Entities;

namespace LMS.Business.Services
{
    public class AnswerService : BaseService
    {
        public AnswerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public TaskAnswer GetById(int answerId)
        {
            var answer = unitOfWork.Answers.Get(answerId);
            if (answer == null)
            {
                throw new EntityNotFoundException<Entities.TaskAnswer>(answerId);
            }

            return answer;
        }
        public System.Threading.Tasks.Task CreateAsync(TaskAnswer answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }
            if (answer.Content == null)
            {
                throw new ArgumentException($"{nameof(Entities.TaskAnswer)}.{nameof(Entities.TaskAnswer.Content)} cannot be null");
            }

            var entry = answer;

            unitOfWork.Answers.Create(entry);

            return unitOfWork.SaveAsync();
        }
        public System.Threading.Tasks.Task UpdateAsync(TaskAnswer answerDto)
        {
            if (answerDto == null)
            {
                throw new ArgumentNullException(nameof(answerDto));
            }

            var newAnswer = answerDto;

            if (unitOfWork.Answers.Get(newAnswer.Id) is Entities.TaskAnswer oldAnswer)
            {
                if (oldAnswer.TaskId == newAnswer.TaskId
                    && oldAnswer.TestSessionUser.Equals(newAnswer.TestSessionUser)
                    && oldAnswer.Content == newAnswer.Content)
                {
                    return System.Threading.Tasks.Task.CompletedTask;
                }
                if (oldAnswer.TaskId != newAnswer.TaskId
                    || !oldAnswer.TestSessionUser.Equals(newAnswer.TestSessionUser))
                {
                    unitOfWork.Answers.Create(newAnswer);
                }
                else
                {
                    unitOfWork.Answers.Update(oldAnswer);
                }
            }
            else
            {
                unitOfWork.Answers.Create(newAnswer);
            }
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TaskAnswer> GetAll()
        {
            var answers = unitOfWork.Answers
                .GetAll();
            return answers;
        }
    }
}
