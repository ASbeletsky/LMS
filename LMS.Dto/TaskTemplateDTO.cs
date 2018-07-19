using System.Collections.Generic;

namespace LMS.Dto
{
    public class TaskTemplateDTO
    {
        public IReadOnlyList<TaskTypeDTO> Types { get; set; }
        public int ValidTaskCount { get; set; }
    }
}
