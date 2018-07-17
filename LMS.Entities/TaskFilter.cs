using System.Collections.Generic;

namespace LMS.Entities
{
    public class TaskFilter
    {
        public int MinComplexity { get; set; }
        public int MaxComplexity { get; set; }
                
        public List<LevelTaskType> TaskTypes { get; set; }
        public List<LevelCategory> Categories { get; set; }
    }
}
