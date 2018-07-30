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
        public AnswersDTO GetById(int answerId)
        {
            var answer = unitOfWork.Answers.Get(answerId);
            if (answer == null)
            {
                throw new EntityNotFoundException<Entities.TaskAnswer>(answerId);
            }

            return mapper.Map<Entities.TaskAnswer, AnswersDTO>(answer);
        }
        public Task CreateAsync(AnswersDTO answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }
            if (string.IsNullOrEmpty(answer.Content))
            {
                throw new ArgumentException($"{nameof(Entities.TaskAnswer)}.{nameof(Entities.TaskAnswer.Content)} cannot be null or empty");
            }

            var entry = mapper.Map<AnswersDTO, Entities.TaskAnswer>(answer);

            unitOfWork.Answers.Create(entry);

            return unitOfWork.SaveAsync();
        }
        public Task UpdateAsync(AnswersDTO answerDto)
        {
            if (answerDto == null)
            {
                throw new ArgumentNullException(nameof(answerDto));
            }

            var newAnswer = mapper.Map<AnswersDTO, Entities.TaskAnswer>(answerDto);

            if (unitOfWork.Answers.Get(newAnswer.Id) is Entities.TaskAnswer oldAnswer)
            {
                if (oldAnswer.Content == newAnswer.Content
                    && oldAnswer.TaskId == newAnswer.TaskId
                    && oldAnswer.Content == newAnswer.Content
                    && oldAnswer.TestSessionUserId != newAnswer.TestSessionUserId)
                {
                    return Task.CompletedTask;
                }
                if (oldAnswer.TaskId != newAnswer.TaskId
                    || oldAnswer.TestSessionUserId != newAnswer.TestSessionUserId)
                {
                    unitOfWork.Answers.Create(newAnswer);
                }
                else {
                    unitOfWork.Answers.Update(oldAnswer);
                }
            }
            else
            {
                unitOfWork.Answers.Create(newAnswer);
            }
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<AnswersDTO> GetAll()
        {
            var answers = unitOfWork.Answers
                .GetAll();
            return mapper.Map<IEnumerable<Entities.TaskAnswer>, IEnumerable<AnswersDTO>>(answers);
        }
    }
}
