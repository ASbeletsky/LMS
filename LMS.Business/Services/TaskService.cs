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

        public Task MarkAsDeletedByIdAsync(int taskId)
        {
            if (unitOfWork.Tasks.Get(taskId) is Entities.Task task)
            {
                task.IsActive = false;

                return unitOfWork.SaveAsync();
            }

            return Task.CompletedTask;
        }

        public TaskDTO GetById(int taskId)
        {
            var task = unitOfWork.Tasks.Get(taskId);
            if (task == null)
            {
                throw new EntityNotFoundException<Entities.Task>(taskId);
            }

            return mapper.Map<Entities.Task, TaskDTO>(task);
        }

        public Task CreateAsync(TaskDTO task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }
            if (string.IsNullOrEmpty(task.Content))
            {
                throw new ArgumentException($"{nameof(Entities.Task)}.{nameof(Entities.Task.Content)} cannot be null or empty");
            }

            var entry = mapper.Map<TaskDTO, Entities.Task>(task);
            entry.IsActive = true;

            unitOfWork.Tasks.Create(entry);

            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TaskDTO taskDto)
        {
            if (taskDto == null)
            {
                throw new ArgumentNullException(nameof(taskDto));
            }

            var newTask = mapper.Map<TaskDTO, Entities.Task>(taskDto);

            if (unitOfWork.Tasks.Get(newTask.Id) is Entities.Task oldTask)
            {
                if (oldTask.Content == newTask.Content
                    && oldTask.Complexity == newTask.Complexity
                    && oldTask.CategoryId == newTask.CategoryId
                    && oldTask.TypeId == newTask.TypeId)
                {
                    return Task.CompletedTask;
                }

                newTask.PreviousVersion = oldTask;
                newTask.IsActive = true;
                newTask.Id = 0;

                oldTask.IsActive = false;

                unitOfWork.Tasks.Update(oldTask);
                unitOfWork.Tasks.Create(newTask);
                return unitOfWork.SaveAsync();
            }
            else
            {
                return CreateAsync(taskDto);
            }
        }

        public IEnumerable<TaskDTO> GetAll(bool includeInactive = false)
        {
            var tasks = unitOfWork.Tasks
                .GetAll();

            if (!includeInactive)
                tasks = tasks.Where(q => q.IsActive);

            return mapper.Map<IEnumerable<Entities.Task>, IEnumerable<TaskDTO>>(tasks);
        }

        public IEnumerable<TaskDTO> GetActiveByFilter(TaskFilterDTO filter)
        {
            var tasks = unitOfWork.Tasks
                .GetAll()
                .Where(t => t.IsActive
                    && t.Complexity >= filter.MinComplexity 
                    && t.Complexity <= filter.MaxComplexity
                    && filter.Categories.Any(c => t.CategoryId == c.Id)
                    && filter.TaskTypes.Any(c => t.TypeId == c.Id));
            
            return mapper.Map<IEnumerable<Entities.Task>, IEnumerable<TaskDTO>>(tasks);
        }
    }
}
