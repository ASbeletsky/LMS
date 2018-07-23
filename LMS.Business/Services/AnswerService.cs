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
        public AnswerDTO GetById(int answerId)
        {
            var answer = unitOfWork.Answers.Get(answerId);
            if (answer == null)
            {
                throw new EntityNotFoundException<Entities.Answer>(answerId);
            }

            return mapper.Map<Entities.Answer, AnswerDTO>(answer);
        }
        public Task CreateAsync(AnswerDTO answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }
            if (string.IsNullOrEmpty(answer.Content))
            {
                throw new ArgumentException($"{nameof(Entities.Answer)}.{nameof(Entities.Answer.Content)} cannot be null or empty");
            }

            var entry = mapper.Map<AnswerDTO, Entities.Answer>(answer);

            unitOfWork.Answers.Create(entry);

            return unitOfWork.SaveAsync();
        }
        public Task UpdateAsync(AnswerDTO answerDto)
        {
            if (answerDto == null)
            {
                throw new ArgumentNullException(nameof(answerDto));
            }

            var newAnswer = mapper.Map<AnswerDTO, Entities.Answer>(answerDto);

            if (unitOfWork.Answers.Get(newAnswer.Id) is Entities.Answer oldAnswer)
            {
                if (oldAnswer.Content == newAnswer.Content
                    && oldAnswer.TaskId == newAnswer.TaskId
                    && oldAnswer.TestId == newAnswer.TestId
                    && oldAnswer.UserId == newAnswer.UserId)
                {
                    return Task.CompletedTask;
                }
                if (oldAnswer.TaskId != newAnswer.TaskId
                    || oldAnswer.TestId != newAnswer.TestId
                    || oldAnswer.UserId != newAnswer.UserId)
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

        public IEnumerable<AnswerDTO> GetAll()
        {
            var answers = unitOfWork.Answers
                .GetAll();
            return mapper.Map<IEnumerable<Entities.Answer>, IEnumerable<AnswerDTO>>(answers);
        }
    }
}
