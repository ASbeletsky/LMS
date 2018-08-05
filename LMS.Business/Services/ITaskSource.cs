using LMS.Dto;
using System.Collections.Generic;

namespace LMS.Business.Services
{
    public interface ITaskSource
    {
        IEnumerable<TaskDTO> GetAll(bool includeInactive = false);
        IEnumerable<TaskDTO> Filter(TaskFilterDTO filter);
    }
}
