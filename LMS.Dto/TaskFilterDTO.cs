using System.Collections.Generic;

namespace LMS.Dto
{
    public class TaskFilterDTO
    {
        public int MinComplexity { get; set; }
        public int MaxComplexity { get; set; }

        public IReadOnlyList<TaskTypeDTO> TaskTypes { get; set; }
        public IReadOnlyList<CategoryDTO> Categories { get; set; }

        public int ValidTaskCount { get; set; }
    }
}
