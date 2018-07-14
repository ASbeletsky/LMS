using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LMS.Dto;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TaskService : BaseService
    {
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public Task MarkAsDeletedByIdAsync(int questionId)
        {
            if (unitOfWork.Tasks.Get(questionId) is Entities.Task question)
            {
                question.IsActive = false;

                return unitOfWork.SaveAsync();
            }

            return Task.CompletedTask;
        }

        public TaskDTO GetById(int questionId)
        {
            var question = unitOfWork.Tasks.Get(questionId);
            if (question == null)
            {
                throw new EntityNotFoundException<Entities.Task>(questionId);
            }

            return mapper.Map<Entities.Task, TaskDTO>(question);
        }

        public Task CreateAsync(TaskDTO question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }
            if (string.IsNullOrEmpty(question.Content))
            {
                throw new ArgumentException($"{nameof(Entities.Task)}.{nameof(Entities.Task.Content)} cannot be null or empty");
            }

            var entry = mapper.Map<TaskDTO, Entities.Task>(question);
            entry.IsActive = true;

            unitOfWork.Tasks.Create(entry);

            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TaskDTO questionDto)
        {
            if (questionDto == null)
            {
                throw new ArgumentNullException(nameof(questionDto));
            }

            var newTask = mapper.Map<TaskDTO, Entities.Task>(questionDto);

            if (unitOfWork.Tasks.Get(newTask.Id) is Entities.Task oldTask)
            {
                if (oldTask.Content == newTask.Content
                    && oldTask.Complexity == newTask.Complexity
                    && oldTask.CategoryId == newTask.CategoryId
                    && oldTask.TypeId == newTask.TypeId)
                {
                    return Task.CompletedTask;
                }

                newTask.IsActive = true;
                newTask.Id = 0;

                oldTask.IsActive = false;

                unitOfWork.Tasks.Update(oldTask);
                unitOfWork.Tasks.Create(newTask);
            }
            else
            {
                unitOfWork.Tasks.Create(newTask);
            }
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TaskDTO> GetAll(bool includeInvisible = false)
        {
            var questions = unitOfWork.Tasks
                .GetAll();

            if (!includeInvisible)
                questions = questions.Where(q => q.IsActive);

            return mapper.Map<IEnumerable<Entities.Task>, IEnumerable<TaskDTO>>(questions);
        }
    }
}
