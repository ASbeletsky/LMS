using LMS.Dto;
using System.Collections.Generic;

namespace LMS.Interfaces
{
    public interface ITaskSource
    {
        IEnumerable<TaskDTO> GetAll(bool includeInactive = false);
        IEnumerable<TaskDTO> GetByFilter(TaskFilterDTO filder);
    }
}
