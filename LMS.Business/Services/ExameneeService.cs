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
        public ExameneeDTO GetById(int exameneeId)
        {
            var examenee = unitOfWork.Examenees.Get(exameneeId);
            if (examenee == null)
            {
                throw new EntityNotFoundException<Entities.Examenee>(exameneeId);
            }

            return mapper.Map<Entities.Examenee, ExameneeDTO>(examenee);
        }
        public Task CreateAsync(ExameneeDTO examenee)
        {
            if (examenee == null)
            {
                throw new ArgumentNullException(nameof(examenee));
            }

            var entry = mapper.Map<ExameneeDTO, Entities.Examenee>(examenee);

            unitOfWork.Examenees.Create(entry);

            return unitOfWork.SaveAsync();
        }
        public Task UpdateAsync(ExameneeDTO exameneeDto)
        {
            if (exameneeDto == null)
            {
                throw new ArgumentNullException(nameof(exameneeDto));
            }

            var newExamenee = mapper.Map<ExameneeDTO, Entities.Examenee>(exameneeDto);

            if (unitOfWork.Examenees.Get(newExamenee.Id) is Entities.Examenee oldExamenee)
            {
                if (oldExamenee.UserId == newExamenee.UserId
                    && oldExamenee.TestId == newExamenee.TestId
                    && oldExamenee.StartTime == newExamenee.StartTime)
                {
                    return Task.CompletedTask;
                }
                if (oldExamenee.UserId != newExamenee.UserId
                    || oldExamenee.TestId != newExamenee.TestId
                    || oldExamenee.StartTime != newExamenee.StartTime)
                {
                    unitOfWork.Examenees.Create(newExamenee);
                }
                else
                {
                    unitOfWork.Examenees.Update(oldExamenee);
                }
            }
            else
            {
                unitOfWork.Examenees.Create(newExamenee);
            }
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<ExameneeDTO> GetAll()
        {
            var answers = unitOfWork.Examenees
                .GetAll();
            return mapper.Map<IEnumerable<Entities.Examenee>, IEnumerable<ExameneeDTO>>(answers);
        }
    }
}
