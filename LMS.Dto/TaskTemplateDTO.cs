using System.Collections.Generic;

namespace LMS.Dto
{
    public class TaskTemplateDTO
    {
        public IList<TaskTypeDTO> Types { get; set; }
        public int ValidTaskCount { get; set; }
    }
}
