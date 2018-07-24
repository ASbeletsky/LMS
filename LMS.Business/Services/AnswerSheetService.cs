using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LMS.Dto;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class AnswerSheetService : BaseService
    {
        public AnswerSheetService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public AnswerSheetDTO GetById(int answerId)
        {
            var answer = unitOfWork.AnswerSheet.Get(answerId);
            if (answer == null)
            {
                throw new EntityNotFoundException<Entities.AnswerSheet>(answerId);
            }

            return mapper.Map<Entities.AnswerSheet, AnswerSheetDTO>(answer);
        }
        public Task CreateAsync(AnswerSheetDTO answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }

            var entry = mapper.Map<AnswerSheetDTO, Entities.AnswerSheet>(answer);

            unitOfWork.AnswerSheet.Create(entry);

            return unitOfWork.SaveAsync();
        }
        public Task UpdateAsync(AnswerSheetDTO answerDto)
        {
            if (answerDto == null)
            {
                throw new ArgumentNullException(nameof(answerDto));
            }

            var newAnswer = mapper.Map<AnswerSheetDTO, Entities.AnswerSheet>(answerDto);

            if (unitOfWork.AnswerSheet.Get(newAnswer.Id) is Entities.AnswerSheet oldAnswer)
            {
                if (oldAnswer.UserId == newAnswer.UserId
                    && oldAnswer.TestId == newAnswer.TestId
                    && oldAnswer.StartTime == newAnswer.StartTime)
                {
                    return Task.CompletedTask;
                }
                if (oldAnswer.UserId != newAnswer.UserId
                    || oldAnswer.TestId != newAnswer.TestId
                    || oldAnswer.StartTime != newAnswer.StartTime)
                {
                    unitOfWork.AnswerSheet.Create(newAnswer);
                }
                else
                {
                    unitOfWork.AnswerSheet.Update(oldAnswer);
                }
            }
            else
            {
                unitOfWork.AnswerSheet.Create(newAnswer);
            }
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<AnswerSheetDTO> GetAll()
        {
            var answers = unitOfWork.AnswerSheet
                .GetAll();
            return mapper.Map<IEnumerable<Entities.AnswerSheet>, IEnumerable<AnswerSheetDTO>>(answers);
        }
    }
}
