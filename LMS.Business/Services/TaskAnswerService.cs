using System;
using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TaskAnswerService : BaseService
    {
        public TaskAnswerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public TaskAnswerDTO GetById(int answerId)
        {
            var answer = unitOfWork.TaskAnswers.Get(answerId);
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
            if (string.IsNullOrEmpty(answer.Content))
            {
                throw new ArgumentException($"{nameof(Entities.TaskAnswer)}.{nameof(Entities.TaskAnswer.Content)} cannot be null or empty");
            }

            var entry = mapper.Map<TaskAnswerDTO, Entities.TaskAnswer>(answer);

            unitOfWork.TaskAnswers.Create(entry);

            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TaskAnswerDTO answerDto)
        {
            if (answerDto == null)
            {
                throw new ArgumentNullException(nameof(answerDto));
            }

            var newAnswer = mapper.Map<TaskAnswerDTO, Entities.TaskAnswer>(answerDto);

            if (unitOfWork.TaskAnswers.Get(newAnswer.Id) is Entities.TaskAnswer oldAnswer)
            {
                if (oldAnswer.TaskId == newAnswer.TaskId
                    && oldAnswer.Score == newAnswer.Score
                    && oldAnswer.Content == newAnswer.Content)
                {
                    return Task.CompletedTask;
                }

                unitOfWork.TaskAnswers.Update(oldAnswer);
                return unitOfWork.SaveAsync();
            }
            else
            {
                return CreateAsync(answerDto);
            }
        }

        public IEnumerable<TaskAnswerDTO> GetAll()
        {
            var answers = unitOfWork.TaskAnswers
                .GetAll();

            return mapper.Map<IEnumerable<Entities.TaskAnswer>, IEnumerable<TaskAnswerDTO>>(answers);
        }
    }
}
