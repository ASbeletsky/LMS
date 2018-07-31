using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LMS.Dto;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class AnswerService : BaseService
    {
        public AnswerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public TaskAnswerDTO GetById(int answerId)
        {
            var answer = unitOfWork.Answers.Get(answerId);
            if (answer == null)
            {
                throw new EntityNotFoundException<Entities.TaskAnswer>(answerId);
            }

            return mapper.Map<Entities.TaskAnswer, TaskAnswerDTO>(answer);
        }
        public Task CreateAsync(TaskAnswerDTO answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }
            if (answer.Content == null)
            {
                throw new ArgumentException($"{nameof(Entities.TaskAnswer)}.{nameof(Entities.TaskAnswer.Content)} cannot be null");
            }

            var entry = mapper.Map<TaskAnswerDTO, Entities.TaskAnswer>(answer);

            unitOfWork.Answers.Create(entry);

            return unitOfWork.SaveAsync();
        }
        public Task UpdateAsync(TaskAnswerDTO answerDto)
        {
            if (answerDto == null)
            {
                throw new ArgumentNullException(nameof(answerDto));
            }

            var newAnswer = mapper.Map<TaskAnswerDTO, Entities.TaskAnswer>(answerDto);

            if (unitOfWork.Answers.Get(newAnswer.Id) is Entities.TaskAnswer oldAnswer)
            {
                if (oldAnswer.TaskId == newAnswer.TaskId
                    && oldAnswer.TestSessionUser.Equals(newAnswer.TestSessionUser)
                    && oldAnswer.Content == newAnswer.Content)
                {
                    return Task.CompletedTask;
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

        public IEnumerable<TaskAnswerDTO> GetAll()
        {
            var answers = unitOfWork.Answers
                .GetAll();
            return mapper.Map<IEnumerable<Entities.TaskAnswer>, IEnumerable<TaskAnswerDTO>>(answers);
        }
    }
}
